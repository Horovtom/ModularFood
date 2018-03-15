using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphEmitter : MonoBehaviour {
    private FunctionsRepository repository = new FunctionsRepository();

    private ParticleSystem.Particle[] particles;

    public ParticleSystem system;

    private bool changed = true;

    private FunctionsRepository.Shape _currentShape = FunctionsRepository.Shape.Oval;
    public FunctionsRepository.Shape currentShape = FunctionsRepository.Shape.Cube;

    private const int MIN_RESOLUTION = 10;
    private const int MAX_RESOLUTION = 10000;
    private int _resolution = MIN_RESOLUTION;
    public int resolution = MIN_RESOLUTION;

    // Use this for initialization
    void Start() {
        CreateParticles();
        currentShape = FunctionsRepository.Shape.Cube;
    }

    // Update is called once per frame
    void Update() {
        system.SetParticles(particles, particles.Length);
        if (_currentShape != currentShape) {
            Debug.Log("Shape changed to: " + currentShape);
            _currentShape = currentShape;
            changed = true;
            repository.SetShape(currentShape);
        }
        if (_resolution != resolution) {
            if (resolution < MIN_RESOLUTION || resolution > MAX_RESOLUTION) {
                Debug.Log("Trying to set Resolution to wrong value! Resetting to minimum");
                resolution = MIN_RESOLUTION;
            }
            Debug.Log("Resolution changed to: " + resolution);
            _resolution = resolution;
            changed = true;
        }

        if (changed) {
            changed = false;
            UpdateParticles();
        }
    }

    /// <summary>
    /// Updates all particles positions, if resolution changed, it will initialize new particles
    /// </summary>
    private void UpdateParticles() {
        Debug.Log("Updating Particles");
        int shouldBe = GetNumParticles();
        if (particles.Length != shouldBe) {
            CreateParticles();
        }

        Vector2 uRange = repository.GetURange(), vRange = repository.GetVRange();
        float uSpan  = uRange.y - uRange.x, vSpan = vRange.y - vRange.x;
        float uRes = uSpan / this.resolution, vRes = vSpan / this.resolution;
        float u, v;
        for (int i = 0; i < this.resolution; i++) {
            u = uRange.x + i * uRes;
            for (int j = 0; j < this.resolution; j++) {
                v = vRange.x + j * vRes;
                ParticleSystem.Particle part = particles[GetIndex(i, j)];
                Vector3 p = repository.GetVect(u, v);
                //Vector3 p = new Vector3((float)i / this.resolution, (float) j / this.resolution, 0);
                part.position = p;
                part.color =Color.red;
                part.size = 0.5f;
            }
        }

        system.SetParticles(particles, particles.Length);
        //GetComponent<ParticleSystem>().SetParticles(particles, particles.Length);
    }

    /// <summary>
    /// Returns number of particles needed for drawing
    /// </summary>
    private int GetNumParticles() {
        return resolution * resolution;
    }

    /// <summary>
    /// Returns 1d index from 2d indices
    /// </summary>
    private int GetIndex(int x, int y) {
        return y * resolution + x;
    }

    /// <summary>
    /// Initializes all particles in the system
    /// </summary>
    private void CreateParticles() {
        Debug.Log("Initializing particles");
        particles = new ParticleSystem.Particle[GetNumParticles()];
        for (int i = 0; i < resolution; i++) {
            for (int j = 0; j < resolution; j++) {
                ParticleSystem.Particle p = particles[GetIndex(i, j)];
                p.size = 0.5f;
                p.position = new Vector3(0, 0, 0f);
                p.startColor = new Color(0, 0, 0f);
            }
        }

        GetComponent<ParticleSystem>().SetParticles(particles, particles.Length);
    }
}
