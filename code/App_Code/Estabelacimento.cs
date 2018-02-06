using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for Estabelacimento
/// </summary>
public class Estabelacimento
{
    String id;
    String nome;
    String password;
    int certidao;
    String coordenadas;
    DateTime abertura;
    DateTime encerramento;
    String tematica;

    public Estabelacimento(String id,  String nome, String password,int certidao, String coordenadas, DateTime abertura, DateTime encerramento, String tematica)
    {
        this.id = id;
        this.nome = nome;
        this.password = password;
        this.certidao = certidao;
        this.coordenadas = coordenadas;
        this.abertura = abertura;
        this.encerramento = encerramento;
        this.tematica = tematica;
    }

    public String toString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(id).Append(nome).Append(password).Append(certidao).Append(coordenadas).Append(abertura).Append(encerramento).Append(tematica);
        return sb.ToString      ();
    }

}