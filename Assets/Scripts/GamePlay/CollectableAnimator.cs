using UnityEngine;

namespace GamePlay

{
    public class CollectableAnimator : MonoBehaviour
    {
        [SerializeField] private bool _isRotating;
        [SerializeField] private bool _rotateX;
        [SerializeField] private bool _rotateY;
        [SerializeField] private bool _rotateZ;
        [SerializeField] private float _rotationSpeed = 90f; // Degrees per second

        [SerializeField] private bool _isFloating;
        [SerializeField] private bool _useEasingForFloating; // Separate toggle for floating ease
        [SerializeField] private float _floatHeight = 1f; // Max height displacement
        [SerializeField] private float _floatSpeed = 1f;

        [SerializeField] private Vector3 _startScale;
        [SerializeField] private Vector3 _endScale;

        [SerializeField] private bool _isScaling;
        [SerializeField] private bool _useEasingForScaling; // Separate toggle for scaling ease
        [SerializeField] private float _scaleLerpSpeed = 1f; // Speed of scaling transition

        private Vector3 _initialScale;
        private float _scaleTimer;
        private Vector3 _initialPosition;
        private float _floatTimer;
        private Transform _transform;

        private Vector3 _rotationVector;
        
        private void Start()
        {
            _transform = transform;
            _initialScale = _transform.localScale;
            _initialPosition = _transform.position;
            _rotationVector = new Vector3(
                _rotateX ? 1 : 0,
                _rotateY ? 1 : 0,
                _rotateZ ? 1 : 0
            );

            // Adjust start and end scale based on initial scale
            _startScale = _initialScale;
            _endScale = _initialScale * (_endScale.magnitude / _startScale.magnitude);
        }

        private void Update()
        {
            Transform transform = _transform;
            
            if (_isRotating)
            {
                transform.Rotate(_rotationVector * _rotationSpeed * Time.deltaTime);
            }

            if (_isFloating)
            {
                _floatTimer += Time.deltaTime * _floatSpeed;
                float t = Mathf.PingPong(_floatTimer, 1f);
                if (_useEasingForFloating) t = EaseInOutQuad(t);

                transform.position = _initialPosition + new Vector3(0, t * _floatHeight, 0);
            }

            if (_isScaling)
            {
                _scaleTimer += Time.deltaTime * _scaleLerpSpeed;
                float t = Mathf.PingPong(_scaleTimer, 1f); // Oscillates between 0 and 1

                if (_useEasingForScaling)
                {
                    t = EaseInOutQuad(t);
                }

                transform.localScale = Vector3.Lerp(_startScale, _endScale, t);
            }
        }

        private float EaseInOutQuad(float t)
        {
            return t < 0.5f ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
        }
    }
}
