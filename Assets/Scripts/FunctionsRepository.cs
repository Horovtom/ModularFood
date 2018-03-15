using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionsRepository {
    public enum Shape {
        Cube,
        Ball,
        Oval,
        Custom
    }
    bool changed = true;

    private Shape currentShape;

    public void SetShape(Shape s) {
        currentShape = s;
        changed = true;
        GetVect(0, 0);
    }

    public Vector3 GetVect(float u, float v) {
        return functionDelegates[(int)currentShape](u, v);
    }

    private Vector2 uRange = new Vector2(), vRange = new Vector2();

    public Vector2 GetURange() {
        return new Vector2(uRange.x, uRange.y);
    }

    public Vector2 GetVRange() {
        return new Vector2(vRange.x, vRange.y);
    }

    private delegate Vector3 FunctionDelegate(float u, float v);

    private FunctionDelegate[] functionDelegates;

    public FunctionsRepository() {
        functionDelegates = new FunctionDelegate[] {
            CubeShape, BallShape, OvalShape, CustomShape
        };
        currentShape = Shape.Cube;
    }

    private Vector3 CubeShape(float u, float v) {
        float x, y, z;
        if (changed ) {
            changed = false;
            uRange = new Vector2(-(Mathf.PI / 2f), Mathf.PI / 2f);
            vRange = new Vector2(0, Mathf.PI * 2);
        }

        if (uRange.x > u || u > uRange.y || vRange.x > v || v > vRange.y) {
            Debug.LogError("U or V were out of bounds! u:" + u + " v: " + v);
            throw new UnityException();
        }
        
        x = Mathf.Cos(u) * Mathf.Cos(v) *
        Mathf.Pow(Mathf.Pow(Mathf.Abs(Mathf.Cos(4 * u / 4f)), 100) + Mathf.Pow(Mathf.Abs(Mathf.Sin(4 * u / 4f)), 100), (-1f / 100f)) *
        Mathf.Pow(Mathf.Pow(Mathf.Abs(Mathf.Cos(4 * v / 4f)), 100) + Mathf.Pow(Mathf.Abs(Mathf.Sin(4 * v / 4f)), 100), (-1f / 100f));
        y = Mathf.Cos(u) * Mathf.Cos(v) *
        Mathf.Pow(Mathf.Pow(Mathf.Abs(Mathf.Cos(4 * u / 4f)), 100) + Mathf.Pow(Mathf.Abs(Mathf.Sin(4 * u / 4f)), 100), (-1f / 100f)) *
        Mathf.Pow(Mathf.Pow(Mathf.Abs(Mathf.Cos(4 * v / 4f)), 100) + Mathf.Pow(Mathf.Abs(Mathf.Sin(4 * v / 4f)), 100), (-1f / 100f));
        z = Mathf.Sin(u) *
        Mathf.Pow(Mathf.Pow(Mathf.Abs(Mathf.Cos(4 * u / 4f)), 100f) +
        Mathf.Pow(Mathf.Abs(Mathf.Sin(4 * u / 4f)), 100f), (-1f / 100f));
        //		x = Mathf.Cos(u) * Mathf.Cos(v) *
        //		(Mathf.Abs(Mathf.Cos(4 * u / 4)) ^ 100 + Mathf.Abs(Mathf.Sin(4 * u / 4)) ^ 100) ^ (-1 / 100) *
        //		(Mathf.Abs(Mathf.Cos(4 * v / 4)) ^ 100 + Mathf.Abs(Mathf.Sin(4 * v / 4)) ^ 100) ^ (-1 / 100);
        //		y = Mathf.Cos(u) * Mathf.Cos(v) *
        //		(Mathf.Abs(Mathf.Cos(4 * u / 4)) ^ 100 + Mathf.Abs(Mathf.Sin(4 * u / 4)) ^ 100) ^ (-1 / 100) *
        //		(Mathf.Abs(Mathf.Cos(4 * v / 4)) ^ 100 + Mathf.Abs(Mathf.Sin(4 * v / 4)) ^ 100) ^ (-1 / 100);
        //		z = Mathf.Sin(u) * (Mathf.Abs(Mathf.Cos(4 * u / 4)) ^ 100 + Mathf.Abs(Mathf.Sin(4 * u / 4)) ^ 100) ^ (-1 / 100);
        
        return new Vector3(x, y, z);
    }

    private Vector3 BallShape(float u, float v) {
        if (changed) {
            changed = false;
            uRange = new Vector2(0, 2 * Mathf.PI);
            vRange = new Vector2(0, Mathf.PI);
        }

        if (uRange.x > u || u > uRange.y || vRange.x > v || v > vRange.y) {
            Debug.LogError("U or V were out of bounds! u:" + u + " v: " + v);
            throw new UnityException();
        }

        float x, y, z;
        x = Mathf.Cos(u) * Mathf.Sin(v);
        y = Mathf.Sin(u) * Mathf.Sin(v);
        z = Mathf.Cos(v);
        
        return new Vector3(x, y, z);
    }

    private Vector3 OvalShape(float u, float v) {
        //TODO: IMPL	
        throw new UnityException();
    }

    private Vector3 CustomShape(float u, float v) {
        //TODO: IMPL
        throw new UnityException();
    }


}
