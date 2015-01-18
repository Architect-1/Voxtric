﻿using UnityEngine;
using VoxelEngine.Hidden;

namespace VoxelEngine.MonoBehaviours
{
    public sealed class ConcaveCollider : MonoBehaviour
    {
        MeshCollider _collider;
        RegionCollection _regionCollection;

        public void Initialise(IntVec3 dataPosition, RegionCollection regionCollection)
        {
            gameObject.name = (string)dataPosition + " Concave Collider";
            gameObject.layer = LayerMask.NameToLayer("Region Collection");
            _regionCollection = regionCollection;
            transform.parent = regionCollection.transform.GetChild(1);
            transform.localRotation = new Quaternion();
            transform.localPosition = (Vector3)dataPosition * VoxelData.SIZE;
            _collider = GetComponent<MeshCollider>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (transform.parent.parent == collision.collider.transform.parent.parent)
            {
                Physics.IgnoreCollision(collision.collider, _collider);
            }
        }

        public void UpdateCollider(Mesh mesh)
        {
            _collider.sharedMesh = null;
            _collider.sharedMesh = mesh;
        }

        public RegionCollection GetRegionCollection()
        {
            return _regionCollection;
        }
    }
}