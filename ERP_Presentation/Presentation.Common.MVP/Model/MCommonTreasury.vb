'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 06-05-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel

#End Region

''' <summary>
''' Modelo que contiene consultas comunes para el modulo de tesoreria
''' </summary>
Public Class MCommonTreasury
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene la secuencia numerica asignada al formulario
    ''' </summary>
    ''' <returns>Secuencia numerica</returns>
    Public Async Function GetSequense() As Task(Of Domain.Entities.TreasurySequence)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetSequenseByIdFormAsync(Me._tagForm)
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por el id de la configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia numerica</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Async Function GetNumericSequenseGroup(ByVal id As Int32) As Task(Of List(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetNumericSequenseGroupByIdAsync(id)
    End Function

    ''' <summary>
    ''' Saves the block record treasury.
    ''' </summary>
    ''' <param name="record">The record.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecordTreasury(ByVal record As BlockRecordTreasury) As Task(Of ActionResult(Of BlockRecordTreasury))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveBlockRecordTreasuryAsync(record)
    End Function

    Public Function SaveBlockRecordTreasurySimple(ByVal record As BlockRecordTreasury) As ActionResult(Of BlockRecordTreasury)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveBlockRecordTreasury(record)
    End Function

    ''' <summary>
    ''' Deletes the block record treasury.
    ''' </summary>
    ''' <param name="record">The record.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecordTreasury(ByVal record As BlockRecordTreasury) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.DeleteBlockRecordTreasuryAsync(record)
    End Function

    ''' <summary>
    ''' Gets the block record treasury.
    ''' </summary>
    ''' <param name="idForm">The identifier form.</param>
    ''' <param name="idRecord">The identifier record.</param>
    ''' <returns></returns>
    Public Async Function GetBlockRecordTreasury(ByVal idForm As String, ByVal idRecord As String) As Task(Of BlockRecordTreasury)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetBlockRecordTreasuryByIdformAndIdRecordAsync(idForm, idRecord)
    End Function

    Public Function GetBlockRecordTreasurySimple(ByVal idForm As String, ByVal idRecord As String) As BlockRecordTreasury
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetBlockRecordTreasuryByIdformAndIdRecord(idForm, idRecord)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
