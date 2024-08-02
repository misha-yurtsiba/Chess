using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class BoardInstaller : MonoInstaller
{
    [SerializeField] private Board boardPrefab;
    [SerializeField] private Tile tilePrefab;

    public override void InstallBindings()
    {
        BoardBind();
        BindTile();
        BoardGeneratorBind();
    }
    private void BoardBind()
    {
        Board board = Container
            .InstantiatePrefabForComponent<Board>(boardPrefab, Vector3.zero, Quaternion.identity,null);

        Container
            .Bind<Board>()
            .FromInstance(board)
            .AsSingle()
            .NonLazy();
    }
    private void BindTile()
    {
        Container
            .Bind<Tile>()
            .FromInstance(tilePrefab)
            .AsSingle();
    }
    private void BoardGeneratorBind()
    {
        Container
            .BindInterfacesAndSelfTo<TileGenerator>()
            .AsSingle()
            .NonLazy();

    
    }

}
