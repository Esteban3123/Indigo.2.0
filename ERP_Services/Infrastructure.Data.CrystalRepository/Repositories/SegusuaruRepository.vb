'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Jhossept K. Garay Rodriguez
' Created          : 11-02-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Domain.Entities
Imports System.Dynamic
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions

Public Class SegusuaruRepository
    Inherits GenericRepository(Of SEGusuaru)
    Implements ISEGusuaruRepository



    'Contexto del repositorio de Indigo Vie Cloud Platform
    Private _crystalContext As ICrystalModelUnitOfWork

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="crystalContext">Contexto</param>
    Public Sub New(ByVal crystalContext As ICrystalModelUnitOfWork)
        MyBase.New(crystalContext)
        Me._crystalContext = crystalContext
    End Sub

    ''' <summary>
    ''' Funcion que retorna un objeto tipo usaurio de crystal
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetusuarioCrystal(code As String) As SEGusuaru Implements ISEGusuaruRepository.GetusuarioCrystal
        Dim Obj = (From e In _crystalContext.SEGusuaru Where e.CODUSUARI = code)
        Return Obj.SingleOrDefault
    End Function

    ''' <summary>
    ''' Changes the password.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <param name="newPassword">The new password.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangePassword(codeUser As String, newPassword As String) As Boolean Implements ISEGusuaruRepository.ChangePassword
        Dim user = (From e In _crystalContext.SEGusuaru Where e.CODUSUARI = codeUser).FirstOrDefault()

        If user IsNot Nothing Then
            user.PASSUSUAR = newPassword
            SaveEntity(user)
        End If

        Return True
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <returns></returns>
    Public Function GetPerfilUbicacion(usuario As String) As ActionResult(Of SP_SEG_AutenticarUsuario_Result) Implements ISEGusuaruRepository.GetPerfilUbicacion
        Dim list = _crystalContext.SP_SEG_AutenticarUsuario(usuario)
        Return New ActionResult(Of SP_SEG_AutenticarUsuario_Result) With {.StateResult = True, .ObjectEmbbeded = list.FirstOrDefault}
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <returns></returns>
    Public Function GetProfesional(usuario As String) As ActionResult(Of SP_SEG_AutenticarDatosProfesional_Result) Implements ISEGusuaruRepository.GetProfesional
        Dim list = _crystalContext.SP_SEG_AutenticarDatosProfesional(usuario)
        Return New ActionResult(Of SP_SEG_AutenticarDatosProfesional_Result) With {.StateResult = True, .ObjectEmbbeded = list.FirstOrDefault}
    End Function

End Class
