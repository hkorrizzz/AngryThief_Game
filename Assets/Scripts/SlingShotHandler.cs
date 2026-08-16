using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class SlingShotHandler : MonoBehaviour
{
    [Header("Line Renderers")]
    [SerializeField] private LineRenderer _leftLineRenderer;
    [SerializeField] private LineRenderer _rightLineRenderer;

    [Header("Transform References")]
    [SerializeField] private Transform _leftStartPosition;
    [SerializeField] private Transform _rightStartPosition;
    [SerializeField] private Transform _centerPosition;
    [SerializeField] private Transform _idlePosition;

    [Header("Slingshot Tsats")]
    [SerializeField] private float _maxDistance = 3.5f;
    [SerializeField] private float _shotForce = 5f;
    [SerializeField] private float _timeBetweenBirdRespawns = 2f;


    [Header("Scripts")]
    [SerializeField] private SlingShootArea _slingShotArea;
    [SerializeField] private CameraManager _cameraManager;

    [Header("Bird")]
    [SerializeField] private AngieBird _angieBirdPrefab;
    [SerializeField] private float _angieBirdPositionOffset = 2f;

    [Header("Sounds")]
    [SerializeField] private AudioClip _clasticPulledClip;
    [SerializeField] private AudioClip[] _clasticReleasedClips;

    private Vector2 _slingShotLinesPosition;

    private Vector2 _direction;
    private Vector2 _directionNrmalized;

    private bool _clickedWithArea;
    private bool _birdOnSlingshot;

    private AngieBird _spanwedAngieBird;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _leftLineRenderer.enabled = false;
        _rightLineRenderer.enabled = false;

        SpawnAngieBird();
    }
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && _slingShotArea.IsWithingSlingshotArea() && _birdOnSlingshot)

        {
            _clickedWithArea = true;

            if (_birdOnSlingshot)
            {
                SoundManager.instance.PlayClip(_clasticPulledClip, _audioSource);
                _cameraManager.SwitchFollowCam(_spanwedAngieBird.transform);
            
            }
        }

        if (Mouse.current.leftButton.isPressed && _clickedWithArea)
        {
            DrawSlingShot();
            PositionAndRotateAngieBird();
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && _birdOnSlingshot)
        {
            if (GameManager.instance.HasEnoughShots()) {
                _clickedWithArea = false;
                _spanwedAngieBird.LaunchBird(_direction, _shotForce);

                SoundManager.instance.PlayRandomClip(_clasticReleasedClips, _audioSource);
               
                
                GameManager.instance.UseShot();
                _birdOnSlingshot = false;
                SetLines(_centerPosition.position);

                if (GameManager.instance.HasEnoughShots())
                {
                    StartCoroutine(SpawnAngieBirdAfterTime());
                }

            }


        }
        
    }

    #region Sling Shot Methods

    private void DrawSlingShot()
    {

        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        _slingShotLinesPosition = _centerPosition.position + Vector3.ClampMagnitude(touchPosition - _centerPosition.position, _maxDistance );
        
        SetLines(_slingShotLinesPosition); 

        _direction = (Vector2)_centerPosition.position - _slingShotLinesPosition;
        _directionNrmalized = _direction.normalized;
    }

    private void SetLines(Vector2 position)
    {
        if (!_leftLineRenderer.enabled && !_rightLineRenderer.enabled)
        {
            _leftLineRenderer.enabled = true;
            _rightLineRenderer.enabled = true;

        }

        _leftLineRenderer.SetPosition(0, position);
        _leftLineRenderer.SetPosition(1, _leftStartPosition.position);

        _rightLineRenderer.SetPosition(0, position);
        _rightLineRenderer.SetPosition(1, _rightStartPosition.position);

    }

    #endregion




    #region Angrie Birds Methods

    private void SpawnAngieBird()
    {
        SetLines(_idlePosition.position);

        Vector2 dir = (_centerPosition.position - _idlePosition.position).normalized;
        Vector2 spawnPosition = (Vector2) _idlePosition.position - dir +_direction * _angieBirdPositionOffset;

        _spanwedAngieBird = Instantiate(_angieBirdPrefab, _idlePosition.position, Quaternion.identity);
        _spanwedAngieBird.transform.right = dir;

        _birdOnSlingshot = true;
    
    }

    private void PositionAndRotateAngieBird()
    {
        _spanwedAngieBird.transform.position = _slingShotLinesPosition + _directionNrmalized * _angieBirdPositionOffset;
        _spanwedAngieBird.transform.right = _directionNrmalized;
    } 

    private IEnumerator SpawnAngieBirdAfterTime()
    {
        yield return new WaitForSeconds(_timeBetweenBirdRespawns);

        SpawnAngieBird();

        _cameraManager.SwitchIdleCam();

    }

    #endregion
}
