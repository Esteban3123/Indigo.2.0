'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Diego Andrés Roldán
' Created          : 24-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Infrastructure.CrossCutting.Exceptions

Public Class ADCENATENRepository
    Inherits GenericRepository(Of ADCENATEN)
    Implements IADCENATENRepository

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

    Public Function GetFirstADCENATEN() As ActionResult(Of ADCENATEN) Implements IADCENATENRepository.GetFirstADCENATEN
        Dim res
        Dim result As New ActionResult(Of ADCENATEN)
        res = (From b In _crystalContext.ADCENATEN.AsNoTracking() Select b).FirstOrDefault()
        If res IsNot Nothing AndAlso Not String.IsNullOrEmpty(res.CODCENATE) Then
            result.ObjectEmbbeded = res
            result.StateResult = True
            Return result
        Else
            result.StateResult = False
            result.Message = "No se encontro ningun centro de atencion."
            Return result
        End If
    End Function

    Public Function GetADCENATENByCode(code As String, tracking As Boolean) As ADCENATEN Implements IADCENATENRepository.GetADCENATENByCode
        Dim res
        Dim codeWithoutSpace = code.Trim()
        If tracking Then
            res = (From b In _crystalContext.ADCENATEN Where b.CODCENATE = codeWithoutSpace Select b).FirstOrDefault()
        Else
            res = (From b In _crystalContext.ADCENATEN.AsNoTracking() Where b.CODCENATE = codeWithoutSpace Select b).FirstOrDefault()
        End If
        If res IsNot Nothing AndAlso Not String.IsNullOrEmpty(res.CODCENATE) Then
            Return res
        Else
            Return New ADCENATEN()
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <param name="grupo"></param>
    ''' <returns></returns>
    Public Function GetCentroAtencionAutorizado(usuario As String, grupo As String) As ActionResult(Of List(Of SP_SEG_CentroAtencion_Autorizado_Result)) Implements IADCENATENRepository.GetCentroAtencionAutorizado
        Dim list = _crystalContext.SP_SEG_CentroAtencion_Autorizado(usuario, grupo)
        Return New ActionResult(Of List(Of SP_SEG_CentroAtencion_Autorizado_Result)) With {.StateResult = True, .ObjectEmbbeded = list.ToList}
    End Function

End Class