using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

	public Node n_up;
	public Node n_left;
	public Node n_down;
	public Node n_right;

    public Vector3 pos;

    void Start()
    {
        pos = gameObject.transform.position;
    }


	void OnDrawGizmosSelected() {
        if (n_up != null) {
			Gizmos.color = Color.red;
			Gizmos.DrawLine(pos, n_up.pos);
		}else if (n_left != null) {
			Gizmos.color = Color.red;
            Gizmos.DrawLine(pos, n_left.pos);
		}else if (n_down != null) {
			Gizmos.color = Color.red;
			Gizmos.DrawLine(pos, n_down.pos);
		}else if (n_right != null) {
			Gizmos.color = Color.red;
			Gizmos.DrawLine(pos, n_right.pos);
		}   
	}

}
