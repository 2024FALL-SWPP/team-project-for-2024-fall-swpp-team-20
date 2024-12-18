using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpawnPositions", order = 1)]
public class SpawnPositions : ScriptableObject
{
    public float origialMoveSpeed;
    public float originalJumpForce;

    public float lavaMoveSpeed;
    public float lavaJumpForce;

    public float chessMoveSpeed;
    public float chessJumpForce;


    public TransformSet chessboardSpawn = new TransformSet(
        new Vector3(4.93f, 0.904f, -0.777f),
        new Vector3(0, 90, 0),
        new Vector3(0.05f, 0.05f, 0.05f)
        );

    public TransformSet lavaSpawn = new TransformSet(
        new Vector3(-19.25f, 1f, -5.382f),
        Vector3.zero,
        0.6f * Vector3.one
    );

    public TransformSet originalSpawn = new TransformSet(
        new Vector3(-19.25f, 0.2f, -7.4f),
        Vector3.zero,
        1.2f * Vector3.one
        );

    public TransformSet visibilitySpawn = new TransformSet(
        new Vector3(3.63f, 0.1f, -3.15f),
        new Vector3(0, 180, 0),
        1.2f * Vector3.one
    );

    public TransformSet reverseSpawn = new TransformSet(
        new Vector3(1.838f, -3.827f, 7.026f),
        new Vector3(0, 180, 0),
        1.2f * Vector3.one
    );
}

public class TransformSet
{
    public Vector3 localPosition;
    public Vector3 eulerRotation;
    public Vector3 scale;

    public TransformSet(Vector3 lp, Vector3 er, Vector3 sc)
    {
        localPosition = lp;
        eulerRotation = er;
        scale = sc;
    }
}
