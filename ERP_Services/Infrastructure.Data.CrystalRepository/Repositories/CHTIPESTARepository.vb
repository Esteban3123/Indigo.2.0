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

Public Class CHTIPESTARepository
    Inherits GenericRepository(Of CHTIPESTA)
    Implements ICHTIPESTARepository

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
    ''' Gets the chtipesta by code.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetCHTIPESTAByCode(code As String) As CHTIPESTA Implements ICHTIPESTARepository.GetCHTIPESTAByCode
        Dim res = (From b In _crystalContext.CHTIPESTA Where b.CODTIPEST = code Select b).FirstOrDefault()
        If res IsNot Nothing AndAlso Not String.IsNullOrEmpty(res.CODTIPEST) Then
            Return res
        Else
            Return New CHTIPESTA()
        End If
    End Function

End Class