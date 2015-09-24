using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class LandZone
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player" && other.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0f)
            {
                other.gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }
    }
}
