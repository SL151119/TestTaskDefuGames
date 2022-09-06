using UnityEngine;

public class СameraFollow : MonoBehaviour
{
	public enum Smooth { Disabled = 0, Enabled = 1 };

	[Header("General")]
	[SerializeField] private float distance; // distance between the camera and the player
	[SerializeField] private float height; // camera height

	[Header("Over The Shoulder")]
	[SerializeField] private float offsetPosition; // camera shift to the right or left, 0 = center

	[Header("Smooth Movement")]
	[SerializeField] private Smooth smooth = Smooth.Enabled;
	[SerializeField] public float speed; // smoothing speed

	private Transform _player;

	private void Start()
	{
		_player = GameObject.FindGameObjectWithTag("Player").transform;
		gameObject.tag = "MainCamera";
	}

	private void Update()
	{
		if (_player)
		{
			// determine the point at a specified distance from the player
			Vector3 position = _player.position - (transform.rotation * Vector3.forward * distance);
			position = position + (transform.rotation * Vector3.right * offsetPosition); // horizontal shift
			position = new Vector3(position.x, _player.position.y + height, position.z); // height adjustment

			if (smooth == Smooth.Disabled)
			{
				transform.position = position;
			}
			else
			{
				transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
			}
		}
	}
}
