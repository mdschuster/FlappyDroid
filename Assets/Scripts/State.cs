﻿/*
Copyright 2020 Micah Schuster

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
this list of conditions and the following disclaimer in the documentation and/or
other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE
OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class State
{
    protected static Play play = new Play();
    protected static GameOver gameOver = new GameOver();
    protected static Reset reset = new Reset();
    protected static Pause pause = new Pause();

    public enum STATE
    {
        PLAY, GAMEOVER, RESET, PAUSE
    }

    public enum EVENT
    {
        EXIT, UPDATE, ENTER
    }

    public STATE name;
    protected EVENT stage;
    protected State nextState;
    protected GameManager gameManager;

    public State()
    {
        gameManager = GameManager.instance();
    }

    public virtual void Enter()
    {
        stage = EVENT.UPDATE;
    }
    public virtual void Update()
    {
        stage = EVENT.UPDATE;
    }
    public virtual void Exit()
    {
        stage = EVENT.EXIT;
    }

    public State process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            nextState.stage = EVENT.ENTER; //because I'm using static classes the constructor is not called again.
            return nextState;
        }
        return this;

    }

}

public class Play : State
{
    public Play() : base()
    {
        name = STATE.PLAY;
        stage = EVENT.ENTER;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        if (gameManager.dead == true)
        {
            nextState = gameOver;
            stage = EVENT.EXIT;
            return;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            nextState = pause;
            gameManager.player.toggleGravity();
            gameManager.moving = false;
            stage = EVENT.EXIT;
            return;
        }
        gameManager.spawnPipePair();
        gameManager.spawnBuilding();
        gameManager.player.checkInput();
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class GameOver : State
{
    public GameOver() : base()
    {
        name = STATE.GAMEOVER;
        stage = EVENT.ENTER;
    }

    public override void Enter()
    {
        gameManager.gameOver();
        gameManager.death();
        base.Enter();
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        nextState = reset;
        base.Exit();
    }
}

public class Reset : State
{
    public Reset() : base()
    {
        name = STATE.RESET;
        stage = EVENT.ENTER;
    }

    public override void Enter()
    {
        gameManager.reset();
        base.Enter();
    }

    public override void Update()
    {
        nextState = play;
        stage = EVENT.EXIT;
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class Pause : State
{
    public Pause() : base()
    {
        name = STATE.RESET;
        stage = EVENT.ENTER;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameManager.player.toggleGravity();
            gameManager.moving = true;

            nextState = play;
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
