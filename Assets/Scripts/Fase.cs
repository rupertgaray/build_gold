﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fase : MonoBehaviour
{
    public Vector3 posInicialPlayer;
    public string nameProximaFase;
    public int mundoAtual, faseAtual;
    public AudioClip btVelocidadeClip;
    public Sprite BTVel1, BTVel2, BTVel3;
    public AudioClip perdeuVida;
    public bool caindo;

    private GameObject[] listaMoedas;

    private GameObject player;
    
    private GameObject sXP,foto, iconFaceCharacter;
    private Text cristais, moedas, total;

    private string cristaisTxt, moedasTxt, totalTxt;
    private Text nome, xp, level;

    private AudioSource audioFase;
    private AudioSource audioPause;
    private AudioSource audioClipFase;
    

    

    public GameObject canvasSucesso, canvasPause, canvasInGame, canvasGameOver;

    public bool mostraMoedas;

    public Slot[] slotsMovimentoIncial;
    public Slot[] slotsMovimentosPlay;
    public Slot[] slotsZerado;

    public GameObject painelWork;
    public GameObject painelInventario;

    public string[] movimentos;

    public GameObject btnPlay, btnVel;

    public float delay;
    public bool entraDelay, podeJogar, fimDeRodada, estaPausado, entraDelayBlocoFalso;
    public int movPos;
    public float pos, speed;
    public int direcao;
    public float countdown, countdownIncial;
    public float timeCountdown;
	public float forcaPuloX;
	public float forcaPuloY;

    public GameObject playerColetador;
    public GameObject cristalFim;


    public GameObject txtPontosFim;
    public GameObject txtCristalFim;
    public GameObject txtMoedaFim;
    public GameObject txtPontosTotalFaseFim;
    public GameObject scrollXPEstagioCompleto;
    public GameObject txtLevel;
    public GameObject txtXPAtual;
    public GameObject txtXPLimite;
    public EstadosPlayer estadoAnterior;

    public GameObject ListaBTMetodos, PainelMovimento, PainelRepeticao;

    public GameObject[] coracoes;

    public GameObject balaObject;
    public Transform balaSpawn;
    public float fareRate, nextFire;

    public GameObject blocoFalso;
    public GameObject blocoFalsoPai;

    public Animator animator;

    public GameObject escada;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Transform>().position = posInicialPlayer;
        playerColetador = GameObject.FindGameObjectWithTag("Coletador");


        audioFase = GameObject.FindGameObjectWithTag("AudioFase").GetComponent<AudioSource>();
        
        audioClipFase = GameObject.FindGameObjectWithTag("AudioClipFase").GetComponent<AudioSource>();
        sXP = GameObject.FindGameObjectWithTag("SliderXP");
        foto = GameObject.FindGameObjectWithTag("IconAvatar");


        iconFaceCharacter = GameObject.FindGameObjectWithTag("IconFace");

        cristais = GameObject.FindGameObjectWithTag("CountCristal").GetComponent<Text>();
        moedas = GameObject.FindGameObjectWithTag("CountCoin").GetComponent<Text>();
        total = GameObject.FindGameObjectWithTag("CountPontuacaoTotal").GetComponent<Text>();

        nome = GameObject.FindGameObjectWithTag("UserName").GetComponent<Text>();
        xp = GameObject.FindGameObjectWithTag("CountXP").GetComponent<Text>();
        level = GameObject.FindGameObjectWithTag("CountLevel").GetComponent<Text>();
        

        cristaisTxt = cristais.text;
        moedasTxt = moedas.text;
        totalTxt = total.text;
        canvasInGame = GameObject.FindGameObjectWithTag("HUDInGame");
        canvasSucesso = GameObject.FindGameObjectWithTag("Sucesso");
        canvasPause = GameObject.FindGameObjectWithTag("PauseGame");
        canvasGameOver = GameObject.FindGameObjectWithTag("GameOver");
        cristalFim = GameObject.FindGameObjectWithTag("Cristal");

        ListaBTMetodos = GameObject.FindGameObjectWithTag("ListaBTMetodos");
        PainelMovimento = GameObject.FindGameObjectWithTag("PainelMovimento");
        PainelRepeticao = GameObject.FindGameObjectWithTag("PainelRepeticao");

        

        txtPontosFim = GameObject.FindGameObjectWithTag("TXTPontosFim");
        txtCristalFim = GameObject.FindGameObjectWithTag("TXTCristalFim");
        txtMoedaFim = GameObject.FindGameObjectWithTag("TXTMoedasFim");
        txtPontosTotalFaseFim = GameObject.FindGameObjectWithTag("TXTPontosTotalFim");
        scrollXPEstagioCompleto = GameObject.FindGameObjectWithTag("ScrollEstagioCompleto");
        txtLevel = GameObject.FindGameObjectWithTag("LevelEstagioCompleto");
        txtXPAtual = GameObject.FindGameObjectWithTag("txtXPAtual");
        txtXPLimite = GameObject.FindGameObjectWithTag("txtXPLimite");

        GameController.instance.AtualizaFaseInicio(nome, xp, level, sXP, foto, player, iconFaceCharacter);
    }

    private void Start()
    {

        switch (GameController.instance.perfilAtivo.GetFasePlayer())
        {
            case FasePlayer.Um:
                {
                    animator.GetComponent<Animator>().SetTrigger("Cena1");
                    faseAtual = 1;
                    break;
                }
            case FasePlayer.Dois:
                {
                    animator.GetComponent<Animator>().SetTrigger("Cena2");
                    faseAtual = 2;
                    break;
                }
            case FasePlayer.Tres:
                {
                    animator.GetComponent<Animator>().SetTrigger("Cena3");
                    faseAtual = 3;
                    break;
                }
            case FasePlayer.Quatro:
                {
                    animator.GetComponent<Animator>().SetTrigger("Cena4");
                    faseAtual = 4;
                    break;
                }
            case FasePlayer.Cinco:
                {
                    animator.GetComponent<Animator>().SetTrigger("Cena5");
                    faseAtual = 5;
                    break;
                }
            default:
                {
                    animator.GetComponent<Animator>().SetTrigger("Cena1");
                    faseAtual = 1;
                    break;
                }
        }

        switch (GameController.instance.perfilAtivo.GetMundoPlayer())
        {
            case MundoPlayer.Madeira:
                {
                    mundoAtual = 1;
                    break;
                }
            case MundoPlayer.Concreto:
                {
                    mundoAtual = 2;
                    break;
                }
            case MundoPlayer.Jade:
                {
                    mundoAtual = 3;
                    break;
                }
            case MundoPlayer.Gelo:
                {
                    mundoAtual = 4;
                    break;
                }
            default:
                {
                    animator.GetComponent<Animator>().SetTrigger("Cena1");
                    faseAtual = 1;
                    break;
                }
        }

        canvasPause.SetActive(false);
        canvasSucesso.SetActive(false);
        canvasGameOver.SetActive(false);

        ListaBTMetodos.SetActive(false);
        PainelMovimento.SetActive(false);
        if(PainelRepeticao != null)
        {
            PainelRepeticao.SetActive(false);
        }
        

        audioFase.Play();
        estadoAnterior = player.GetComponent<Player>().estadoAtual;

        listaMoedas = GameObject.FindGameObjectsWithTag("Coin");
        caindo = false;
        


        painelWork = GameObject.FindGameObjectWithTag("PainelBtWorkstation");
        painelInventario = GameObject.FindGameObjectWithTag("PainelMetodos");
        
        slotsZerado = painelWork.GetComponentsInChildren<Slot>();
        btnPlay = GameObject.FindGameObjectWithTag("BtPlay");
        btnVel = GameObject.FindGameObjectWithTag("BtVel");
        blocoFalso = GameObject.FindGameObjectWithTag("BlocoFalso");
        blocoFalsoPai = GameObject.FindGameObjectWithTag("BlocoFalsoPai");

        podeJogar = false;
        direcao = 1;
        countdown = timeCountdown;
        countdownIncial = timeCountdown + 5;
        fimDeRodada = false;
        cristalFim.SetActive(false);
        estaPausado = false;
        coracoes = GameObject.FindGameObjectsWithTag("Coracao");


        
    }

    private void Update()
    {

        if(GameController.instance.ativaTutorial == false)
        {
            if (estaPausado == false)
            {
                movimentos = painelInventario.GetComponent<Inventory>().ListarMovimentos();
                if (movimentos.Length > 1 && podeJogar == false)
                {
                    btnPlay.GetComponent<Button>().interactable = true;
                }
                else
                {
                    btnPlay.GetComponent<Button>().interactable = false;
                }

                if (MostraCristal())
                {
                    cristalFim.SetActive(true);
                }

                bool blocoFalsoAtivo = false;
                VerificaMoedasFases();
                if (blocoFalso != null)
                {
                    blocoFalsoAtivo = blocoFalso.GetComponent<ColisorBlocoFalso>().ativo;
                }


                if (entraDelay)
                {
                    countdown -= Time.deltaTime;
                    if (countdown <= 0.0f)
                    {
                        entraDelay = false;
                        if (fimDeRodada)
                        {
                            VerificaResultado();
                        }
                    }
                }
                else if (entraDelayBlocoFalso)
                {
                    blocoFalsoPai.GetComponent<BoxCollider2D>().enabled = false;
                    countdown -= Time.deltaTime;
                    if (countdown <= 0.0f)
                    {
                        VerificaResultado();
                    }
                }
                else
                {

                    if (blocoFalsoAtivo)
                    {
                        entraDelay = false;
                        entraDelayBlocoFalso = true;
                        countdown = 1.5f;
                        Debug.Log("Bloco falso ativo");
                    }
                    else
                    {
                        if (podeJogar)
                        {
                            entraDelay = true;
                            ExecutaMovimento(ProximoMovimento());

                        }
                    }
                }
            }
        }
    }

    private void VerificaResultado()
    {
        bool capturado = cristalFim.GetComponent<Item>().capturado;
        if (capturado == true)
        {
            Debug.Log("Cristal foi Capturado - Fim");
            
            canvasSucesso.SetActive(true);
            canvasSucesso.GetComponent<AudioSource>().Play();
            scrollXPEstagioCompleto = GameObject.FindGameObjectWithTag("ScrollEstagioCompleto");
            //txtLevel = GameObject.FindGameObjectWithTag("LevelEstagioCompleto");
            txtPontosFim.GetComponent<Text>().text = (int.Parse(total.text) + 100).ToString();
            txtCristalFim.GetComponent<Text>().text = cristais.text;
            txtMoedaFim.GetComponent<Text>().text = moedas.text;
            int val = int.Parse(txtPontosFim.GetComponent<Text>().text) * 2;
            Debug.Log(val);
            txtPontosTotalFaseFim.GetComponent<Text>().text = val.ToString();

            GameController.instance.AtualizaXPEstagioCompleto(val, scrollXPEstagioCompleto, txtLevel.GetComponent<Text>(), txtXPAtual.GetComponent<Text>(), txtXPLimite.GetComponent<Text>().GetComponent<Text>(), mundoAtual, faseAtual, int.Parse(txtCristalFim.GetComponent<Text>().text), int.Parse(txtMoedaFim.GetComponent<Text>().text));

        }
        else if(capturado == false)
        {
            if(player.GetComponent<Player>().colisorFim == true)
            {
                canvasSucesso.SetActive(true);
                scrollXPEstagioCompleto = GameObject.FindGameObjectWithTag("ScrollEstagioCompleto");
                //txtLevel = GameObject.FindGameObjectWithTag("LevelEstagioCompleto");
                txtPontosFim.GetComponent<Text>().text = (int.Parse(total.text) + 100).ToString();
                txtCristalFim.GetComponent<Text>().text = cristais.text;
                txtMoedaFim.GetComponent<Text>().text = moedas.text;
                int val = int.Parse(txtPontosFim.GetComponent<Text>().text);
                
                txtPontosTotalFaseFim.GetComponent<Text>().text = val.ToString();

                GameController.instance.AtualizaXPEstagioCompleto(val, scrollXPEstagioCompleto, txtLevel.GetComponent<Text>(), txtXPAtual.GetComponent<Text>(), txtXPLimite.GetComponent<Text>().GetComponent<Text>(), mundoAtual, faseAtual, int.Parse(txtCristalFim.GetComponent<Text>().text), int.Parse(txtMoedaFim.GetComponent<Text>().text));
            }
            else
            {
                //ReiniciarFase();
                Debug.Log("Não chegou ao fim da fase! - Game Over");
                if(GameController.instance.perfilAtivo.GetVidas() == 1)
                {
                    audioFase.Stop();
                    canvasInGame.SetActive(false);
                    canvasGameOver.SetActive(true);
                    canvasGameOver.GetComponent<AudioSource>().Play();
                    entraDelayBlocoFalso = false;
                    //player.GetComponent<Player>().estadoAtual = EstadosPlayer.Morto;
                }
                else
                {
                    Debug.Log("Não chegou ao fim da fase! - Perdeu vida");
                    entraDelayBlocoFalso = false;
                    audioClipFase.clip = perdeuVida;
                    audioClipFase.Play();
                    GameController.instance.perfilAtivo.DiminuiVida();
                    RemoveCoracao(GameController.instance.perfilAtivo.GetVidas());
                    ReiniciarFase();
                }
            }
        }
    }

    private void RemoveCoracao(int v)
    {
        switch (v)
        {
            case 3:
                {
                    foreach(GameObject obj in coracoes)
                    {
                        string[] name = obj.name.Split('-');

                        if (name[3].CompareTo("1") == 0)
                        {
                            obj.SetActive(false);
                        }
                    }
                    break;
                }
            case 2:
                {
                    foreach (GameObject obj in coracoes)
                    {
                        string[] name = obj.name.Split('-');

                        if (name[3].CompareTo("2") == 0)
                        {
                            obj.SetActive(false);
                        }
                    }
                    break;
                }
                case 1:
                {
                    foreach (GameObject obj in coracoes)
                    {
                        string[] name = obj.name.Split('-');

                        if (name[3].CompareTo("3") == 0)
                        {
                            obj.SetActive(false);
                        }
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    private void ReiniciarFase()
    {
        player.GetComponent<Transform>().position = posInicialPlayer;
        movPos = 0;
        podeJogar = false;
        btnVel.GetComponent<Button>().interactable = true;
        ListaBTMetodos.SetActive(false);
        PainelMovimento.SetActive(false);
        if(PainelRepeticao != null)
        {
            PainelRepeticao.SetActive(false);
        }
        
        fimDeRodada = false;
        if(blocoFalso != null)
        {
            blocoFalso.GetComponent<ColisorBlocoFalso>().ativo = false;
        }
        
        caindo = false;
    }

    private void VerificaMoedasFases()
    {
        if (mostraMoedas)
        {
            foreach (GameObject m in listaMoedas)
            {
                m.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject m in listaMoedas)
            {
                m.gameObject.SetActive(true);
            }
        }
    }

    private bool MostraCristal()
    {
        foreach (GameObject go in listaMoedas)
        {
            if (go.GetComponent<Item>().capturado == false)
            {
                return false;
            }
        }
        return true;
    }

    private void ExecutaMovimento(string v)
    {
        switch (v)
        {
            case "Mover":
                {
                    Debug.Log("Mover");
                    //player.GetComponent<Player>().estadoAtual = EstadosPlayer.Movendo;
                    Debug.Log("Força x: " + pos);
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2((GetComponent<Transform>().transform.position.x + pos) * direcao, GetComponent<Transform>().transform.position.y));
                    countdown = timeCountdown;
                    entraDelay = true;
                    break;
                }
            case "Descer":
                {
                    Debug.Log("Descer");
                    escada.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    countdown = timeCountdown;
                    entraDelay = true;
                    break;
                }
		    case "Pular":
			    {
                    Debug.Log("Pular");
                    player.GetComponent<Player>().estadoAtual = EstadosPlayer.Pulando;
				    player.GetComponent<Rigidbody2D>().AddForce(new Vector2((forcaPuloX * direcao), forcaPuloY));
                    countdown = timeCountdown;
                    entraDelay = true;
				    break;
			    }

            case "Pegar":
                {
                    Debug.Log("Pegar");
                    player.GetComponent<Player>().estadoAtual = EstadosPlayer.Pegando;
                    playerColetador.GetComponent<BoxCollider2D>().enabled = true;
                    countdown = timeCountdown;
                    entraDelay = true;
                    break;
                }
            case "Fim":
                {
                    player.GetComponent<Player>().estadoAtual = EstadosPlayer.Parado;
                    Debug.Log("Fim da rodada");
                    countdown = 1.5f;
                    entraDelay = true;
                    fimDeRodada = true;
                    podeJogar = false;
                    break;
                }
            case "Pause":
                {
                    player.GetComponent<Player>().estadoAtual = EstadosPlayer.Pausado;
                    estadoAnterior = player.GetComponent<Player>().estadoAtual;
                    Debug.Log("Entrou em Pause");
                    break;
                }
            case "Resumir":
                {
                    player.GetComponent<Player>().estadoAtual = estadoAnterior;
                    Debug.Log("Entrou em Pause");
                    break;
                }
            case "Atirar":
                {
                    player.GetComponent<Player>().estadoAtual = EstadosPlayer.Atirando;
                    Debug.Log("Atirou");
                    if(direcao == 1)
                    {
                        balaSpawn.position = new Vector3(player.transform.position.x + 0.1f, balaSpawn.position.y, balaSpawn.position.z);
                        GameObject cloneBala = Instantiate(balaObject, balaSpawn.position, balaSpawn.rotation);
                    }
                    else
                    {
                        balaSpawn.position = new Vector3(player.transform.position.x - 0.1f, balaSpawn.position.y, balaSpawn.position.z);
                        GameObject cloneBala = Instantiate(balaObject, balaSpawn.position, balaSpawn.rotation);
                        cloneBala.transform.eulerAngles = new Vector3(0, 0, 180);
                    }
                    entraDelay = true;
                    break;
                }
            case "VirarDireita":
                {
                    player.GetComponent<Animator>().enabled = false;
                    player.GetComponent<Player>().estadoAtual = EstadosPlayer.VirarDireita;
                    direcao = 1;
                    player.GetComponent<SpriteRenderer>().flipX = false;
                    Debug.Log("Virou: Direita");
                    entraDelay = true;
                    break;
                }
            case "VirarEsquerda":
                {
                    player.GetComponent<Animator>().enabled = false;
                    player.GetComponent<Player>().estadoAtual = EstadosPlayer.VirarEsquerda;
                    direcao = -1;
                    player.GetComponent<SpriteRenderer>().flipX = true;
                    Debug.Log("Virou: Esquerda");
                    entraDelay = true;
                    break;
                }
            default:
                player.GetComponent<Player>().estadoAtual = EstadosPlayer.Parado;
                break;
        }
        
        
    }

    public void Play()
    {
        movimentos = painelInventario.GetComponent<Inventory>().ListarMovimentos();
        btnPlay.GetComponent<Button>().interactable = false;
        btnPlay.GetComponent<AudioSource>().Play();
        btnVel.GetComponent<Button>().interactable = false;
        
        ListaBTMetodos.SetActive(false);
        PainelMovimento.SetActive(false);
        if (PainelRepeticao != null)
        {
            PainelRepeticao.SetActive(false);
        }
        podeJogar = true;
        //delayInicial = true;

        //Debug.Log(movimentos[0].ToString());
    }

    private string ProximoMovimento()
    {
        string moveTxt = movimentos[movPos].ToString();
        movPos = movPos + 1;
        return (moveTxt.Trim());
    }

    public void ProximaFase()
    {
        SceneManager.LoadScene(nameProximaFase);
    }

    public void ReiniciarFase(GameObject go)
    {
        Debug.LogWarning("Reniciou");
        if (go.activeSelf == true)
        {
            audioFase.Play();
            cristais.text = cristaisTxt;
            moedas.text = moedasTxt;
            total.text = totalTxt;
            player.GetComponent<Transform>().position = posInicialPlayer;
            foreach (GameObject m in listaMoedas)
            {
                m.gameObject.SetActive(true);
                m.GetComponent<Renderer>().enabled = true;
                m.GetComponent<CircleCollider2D>().enabled = true;
                Debug.LogWarning("Entrou aqui");
            }
        }
        go.SetActive(false);
    }

    public void ReiniciarFaseTotal()
    {
        string nomeFase = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nomeFase);
        
    }

    public void ReiniciarFaseTotalSucesso()
    {
        string nomeFase = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nomeFase);

    }

    public void AcaoBotaoPause(GameObject go)
    {
        if (go.activeSelf == false)
        {
            estaPausado = true;
            go.SetActive(true);
            //GameObject player = GameObject.FindGameObjectWithTag("Player");
            audioPause = GameObject.FindGameObjectWithTag("AudioPause").GetComponent<AudioSource>();
            player.GetComponent<Player>().Pause();
            audioPause.Play();
            audioFase.Pause();
        }
    }

    public void AcaoBotaoResume(GameObject go)
    {
        if (go.activeSelf == true)
        {
            estaPausado = false;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Player>().Despause();
            go.SetActive(false);
            audioPause.Stop();
            audioFase.Play();
        }
    }

    public void AcaoBotaoOpenTips(GameObject go)
    {
        if (go.activeSelf == false)
        {
            go.gameObject.SetActive(true);
        }
        else
        {
            go.gameObject.SetActive(false);
        }
    }

    public void AcaoBotaoVel(GameObject go)
    {
        audioClipFase.clip = btVelocidadeClip;
        audioClipFase.Play();
        if (go.GetComponent<Button>().GetComponent<Image>().sprite.name.CompareTo("btn-speed-x1") == 0)
        {
            go.GetComponent<Button>().GetComponent<Image>().sprite = BTVel2;
            AlteraVelocidade(2);
        }
        else if (go.GetComponent<Button>().GetComponent<Image>().sprite.name.CompareTo("btn-speed-x2") == 0)
        {
            go.GetComponent<Button>().GetComponent<Image>().sprite = BTVel3;
            AlteraVelocidade(1);
        }
        else if (go.GetComponent<Button>().GetComponent<Image>().sprite.name.CompareTo("btn-speed-x3") == 0)
        {
            go.GetComponent<Button>().GetComponent<Image>().sprite = BTVel1;
            AlteraVelocidade(3);
        }

    }

    public void AlteraVelocidade(float vel)
    {
        timeCountdown = vel;
    }

    public void Home()
    {
        SceneManager.LoadScene("03_01_SelectTowers");
    }

    public void HomeSucesso()
    {
        SceneManager.LoadScene("03_01_SelectTowers");
    }

    /*public void AcaoBotaoMenuMetodos(GameObject go)
    {
        GameController.instance.ExecutaClip("");
        if (go.activeSelf == false)
        {
            go.gameObject.SetActive(true);
        }
        else
        {
            go.gameObject.SetActive(false);
        }
    }*/

    public void AcaoBtMetodos(GameObject go)
    {
        GameController.instance.ExecutaClip("Metodos");
        if (go.activeSelf == false)
        {
            go.gameObject.SetActive(true);
        }
        else
        {
            go.gameObject.SetActive(false);
        }
    }

    public void BlocoFalso()
    {
        caindo = true;
    }

    public void AcaoProximaFase(int i)
    {
        switch (i)
        {
            /*case 1:
                {
                    GameController.instance.perfilAtivo.mundoAtual = MundoPlayer.Madeira;
                    GameController.instance.perfilAtivo.faseAtual = FasePlayer.Dois;
                    SceneManager.LoadScene("04_01_towerWood");
                    break;
                }*/

            case 2:
                {
                    GameController.instance.perfilAtivo.mundoAtual = MundoPlayer.Madeira;
                    GameController.instance.perfilAtivo.faseAtual = FasePlayer.Dois;
                    SceneManager.LoadScene("04_02_towerWood");
                    break;
                }
            case 3:
                {
                    GameController.instance.perfilAtivo.mundoAtual = MundoPlayer.Madeira;
                    GameController.instance.perfilAtivo.faseAtual = FasePlayer.Tres;
                    SceneManager.LoadScene("04_03_towerWood");
                    break;
                }
            case 4:
                {
                    GameController.instance.perfilAtivo.mundoAtual = MundoPlayer.Madeira;
                    GameController.instance.perfilAtivo.faseAtual = FasePlayer.Quatro;
                    SceneManager.LoadScene("04_04_towerWood");
                    break;
                }
            case 5:
                {
                    GameController.instance.perfilAtivo.mundoAtual = MundoPlayer.Madeira;
                    GameController.instance.perfilAtivo.faseAtual = FasePlayer.Cinco;
                    SceneManager.LoadScene("04_05_towerWood");
                    break;
                }
            case 6:
                {
                    GameController.instance.perfilAtivo.mundoAtual = MundoPlayer.Concreto;
                    GameController.instance.perfilAtivo.faseAtual = FasePlayer.Um;
                    SceneManager.LoadScene("05_01_towerConcrete");
                    break;
                }
            case 7:
                {
                    GameController.instance.perfilAtivo.mundoAtual = MundoPlayer.Concreto;
                    GameController.instance.perfilAtivo.faseAtual = FasePlayer.Dois;
                    SceneManager.LoadScene("05_02_towerConcrete");
                    break;
                }
            case 8:
                {
                    GameController.instance.perfilAtivo.mundoAtual = MundoPlayer.Concreto;
                    GameController.instance.perfilAtivo.faseAtual = FasePlayer.Tres;
                    SceneManager.LoadScene("05_03_towerConcrete");
                    break;
                }
            case 9:
                {
                    GameController.instance.perfilAtivo.mundoAtual = MundoPlayer.Concreto;
                    GameController.instance.perfilAtivo.faseAtual = FasePlayer.Quatro;
                   SceneManager.LoadScene("05_04_towerConcrete");
                    break;
                }
            case 10:
                {
                    GameController.instance.perfilAtivo.mundoAtual = MundoPlayer.Concreto;
                    GameController.instance.perfilAtivo.faseAtual = FasePlayer.Cinco;
                    SceneManager.LoadScene("05_05_towerConcrete");
                    break;
                }
            default:
                {
                    Debug.Log("Opção Inválida");
                    break;
                }
        }
    }

}
