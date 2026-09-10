'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Diego Andrés Roldán
' Created          : 24-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions

Public Class INUNIFUNCRepository
    Inherits GenericRepository(Of INUNIFUNC)
    Implements IINUNIFUNCRepository

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

    Public Function GetINUNIFUNCByCode(code As String) As INUNIFUNC Implements IINUNIFUNCRepository.GetINUNIFUNCByCode
        Dim res = (From b In _crystalContext.INUNIFUNC Where b.UFUCODIGO = code Select b).FirstOrDefault()
        If res IsNot Nothing AndAlso Not String.IsNullOrEmpty(res.UFUCODIGO) Then
            Return res
        Else
            Return New INUNIFUNC()
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <param name="grupo"></param>
    ''' <param name="centroAtencion"></param>
    ''' <returns></returns>
    Public Function GetUnidadFuncionalAutorizado(usuario As String, grupo As String, centroAtencion As String) As ActionResult(Of List(Of SP_SEG_UnidadFuncional_Autorizado_Result)) Implements IINUNIFUNCRepository.GetUnidadFuncionalAutorizado
        Dim list = _crystalContext.SP_SEG_UnidadFuncional_Autorizado(usuario, grupo, centroAtencion)
        Return New ActionResult(Of List(Of SP_SEG_UnidadFuncional_Autorizado_Result)) With {.StateResult = True, .ObjectEmbbeded = list.ToList}
    End Function

End Class