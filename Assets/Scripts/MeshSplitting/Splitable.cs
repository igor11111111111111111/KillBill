using MeshSplitting.MeshTools;
using MeshSplitting.SplitterMath;
using System;
using UnityEngine;

namespace MeshSplitting.Splitables
{
    [AddComponentMenu("Mesh Splitting/Splitable")]
    public class Splitable : MonoBehaviour
    {
        public GameObject OptionalTargetObject;
        public bool Convex = false;
        public float SplitForce = 0f;

        public bool CreateCap = true;
        public bool UseCapUV = false;
        public bool CustomUV = false;
        public Vector2 CapUVMin = Vector2.zero;
        public Vector2 CapUVMax = Vector2.one;

        public bool ForceNoBatching = false;

        private PlaneMath _splitPlane;
        private MeshContainer[] _meshContainerStatic;
        private IMeshSplitter[] _meshSplitterStatic;
        private MeshContainer[] _meshContainerSkinned;
        private IMeshSplitter[] _meshSplitterSkinned;

        private bool _isSplitting = false;
        [SerializeField] private SkinnedMeshRenderer[] _skinnedMeshRenderer;
        [SerializeField] private MeshFilter[] _meshFilter;
        [SerializeField] private Animator _animator;
        [SerializeField] private BoxCollider _boxCollider;

        public void Split(Transform splitTransform)
        {
            if (!_isSplitting)
            {
                _animator.enabled = false;
                var collider = GetComponent<MeshCollider>();
                var mesh = new Mesh();
                _skinnedMeshRenderer[0].BakeMesh(mesh, false);
                collider.sharedMesh = mesh;

                _isSplitting = true;
                _splitPlane = new PlaneMath(splitTransform);

                MeshFilter[] meshFilters = _meshFilter;
                SkinnedMeshRenderer[] skinnedRenderes = _skinnedMeshRenderer;

                _meshContainerStatic = new MeshContainer[meshFilters.Length];
                _meshSplitterStatic = new IMeshSplitter[meshFilters.Length];

                for (int i = 0; i < meshFilters.Length; i++)
                {
                    _meshContainerStatic[i] = new MeshContainer(meshFilters[i]);

                    _meshSplitterStatic[i] = Convex ? (IMeshSplitter)new MeshSplitterConvex(_meshContainerStatic[i], _splitPlane, splitTransform.rotation) :
                                                      (IMeshSplitter)new MeshSplitterConcave(_meshContainerStatic[i], _splitPlane, splitTransform.rotation);

                    if (UseCapUV) _meshSplitterStatic[i].SetCapUV(UseCapUV, CustomUV, CapUVMin, CapUVMax);
                }

                _meshSplitterSkinned = new IMeshSplitter[skinnedRenderes.Length];
                _meshContainerSkinned = new MeshContainer[skinnedRenderes.Length];

                for (int i = 0; i < skinnedRenderes.Length; i++)
                {
                    _meshContainerSkinned[i] = new MeshContainer(skinnedRenderes[i]);

                    _meshSplitterSkinned[i] = Convex ? (IMeshSplitter)new MeshSplitterConvex(_meshContainerSkinned[i], _splitPlane, splitTransform.rotation) :
                                                      (IMeshSplitter)new MeshSplitterConcave(_meshContainerSkinned[i], _splitPlane, splitTransform.rotation);

                    if (UseCapUV) _meshSplitterSkinned[i].SetCapUV(UseCapUV, CustomUV, CapUVMin, CapUVMax);
                }
            }

            bool anySplit = false;

            for (int i = 0; i < _meshContainerStatic.Length; i++)
            {
                _meshContainerStatic[i].MeshInitialize();
                _meshContainerStatic[i].CalculateWorldSpace();

                _meshSplitterStatic[i].MeshSplit();

                if (_meshContainerStatic[i].IsMeshSplit())
                {
                    anySplit = true;
                    if (CreateCap) _meshSplitterStatic[i].MeshCreateCaps();
                }
            }

            for (int i = 0; i < _meshContainerSkinned.Length; i++)
            {
                _meshContainerSkinned[i].MeshInitialize();
                _meshContainerSkinned[i].CalculateWorldSpace();

                // split mesh
                _meshSplitterSkinned[i].MeshSplit();

                if (_meshContainerSkinned[i].IsMeshSplit())
                {
                    anySplit = true;
                    if (CreateCap) _meshSplitterSkinned[i].MeshCreateCaps();
                }
            }

            if (anySplit) CreateNewObjects();
            _isSplitting = false;
        }

        private void CreateNewObjects()
        {
            GameObject[] newGOs = new GameObject[2];
            if (OptionalTargetObject == null)
            {
                newGOs[0] = Instantiate(gameObject);
                newGOs[0].name = gameObject.name;
                newGOs[1] = gameObject;
            }
            else
            {
                newGOs[0] = Instantiate(OptionalTargetObject);
                newGOs[1] = Instantiate(OptionalTargetObject);
            }

            for (int i = 0; i < 2; i++)
            {
                UpdateMeshesInChildren(i, newGOs[i]);

                Transform newTransform = newGOs[i].GetComponent<Transform>();
                newTransform.parent = transform.parent;

                Mesh newMesh = GetMeshOnGameObject(newGOs[i]);
                if (newMesh != null)
                {
                    MeshCollider newCollider = newGOs[i].GetComponent<MeshCollider>();
                    if (newCollider != null)
                    {
                        newCollider.sharedMesh = newMesh;
                        newCollider.convex = false;

                        if (newCollider.convex && newMesh.triangles.Length > 765)
                            newCollider.convex = false;
                    }
                }
            }
            PostProcessObject(newGOs);
        }

        private void UpdateMeshesInChildren(int i, GameObject go)
        {
            if (_meshContainerStatic.Length > 0)
            {
                MeshFilter[] meshFilters = go.transform.GetChild(0).GetComponentsInChildren<MeshFilter>();
                for (int j = 0; j < _meshContainerStatic.Length; j++)
                {
                    Renderer renderer = meshFilters[j].GetComponent<Renderer>();
                    if (ForceNoBatching)
                    {
                        renderer.materials = renderer.materials;
                    }
                    if (i == 0)
                    {
                        if (_meshContainerStatic[j].HasMeshUpper() & _meshContainerStatic[j].HasMeshLower())
                        {
                            meshFilters[j].mesh = _meshContainerStatic[j].CreateMeshUpper();
                        }
                        else if (!_meshContainerStatic[j].HasMeshUpper())
                        {
                            if (renderer != null) Destroy(renderer);
                            Destroy(meshFilters[j]);
                        }
                    }
                    else
                    {
                        if (_meshContainerStatic[j].HasMeshUpper() & _meshContainerStatic[j].HasMeshLower())
                        {
                            meshFilters[j].mesh = _meshContainerStatic[j].CreateMeshLower();
                        }
                        else if (!_meshContainerStatic[j].HasMeshLower())
                        {
                            if (renderer != null) Destroy(renderer);
                            Destroy(meshFilters[j]);
                        }
                    }
                }
            }

            if (_meshContainerSkinned.Length > 0)
            {
                SkinnedMeshRenderer[] skinnedRenderer = go.transform.GetChild(0).GetComponentsInChildren<SkinnedMeshRenderer>();
                for (int j = 0; j < _meshContainerSkinned.Length; j++)
                {
                    if (i == 0)
                    {
                        if (_meshContainerSkinned[j].HasMeshUpper() & _meshContainerSkinned[j].HasMeshLower())
                        {
                            skinnedRenderer[j].sharedMesh = _meshContainerSkinned[j].CreateMeshUpper();
                        }
                        else if (!_meshContainerSkinned[j].HasMeshUpper())
                        {
                            Destroy(skinnedRenderer[j]);
                        }
                    }
                    else
                    {
                        if (_meshContainerSkinned[j].HasMeshUpper() & _meshContainerSkinned[j].HasMeshLower())
                        {
                            skinnedRenderer[j].sharedMesh = _meshContainerSkinned[j].CreateMeshLower();
                        }
                        else if (!_meshContainerSkinned[j].HasMeshLower())
                        {
                            Destroy(skinnedRenderer[j]);
                        }
                    }
                }
            }
        }

        private Mesh GetMeshOnGameObject(GameObject go)
        {
            SkinnedMeshRenderer skinnedMeshRenderer = go.GetComponent<SkinnedMeshRenderer>();
            if (skinnedMeshRenderer != null)
            {
                return skinnedMeshRenderer.sharedMesh;
            }
            else
            {
                MeshFilter meshFilter = go.GetComponent<MeshFilter>();
                if (meshFilter != null)
                {
                    return meshFilter.mesh;
                }
            }

            return null;
        }

        protected virtual void PostProcessObject(GameObject[] gos) 
        {
            foreach (var go1 in gos)
            {
                Destroy(go1.GetComponent<BoxCollider>());
                var go = go1.transform.GetChild(0).transform.GetChild(1).gameObject;
                var mesh = new Mesh();
                go.GetComponent<SkinnedMeshRenderer>().BakeMesh(mesh);
                var meshCollider = go1.GetComponent<MeshCollider>();
                meshCollider.sharedMesh = mesh;
                meshCollider.convex = true;
                meshCollider.enabled = true;
            }
        }
    }
}
