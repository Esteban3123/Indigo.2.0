'***********************************************************************
' Assembly         : Presentation.MixingStation.MVP
' Author           : Yoe Andres Cardenas
' Created          : 29-04-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
#End Region

Public Class MBlockRecordAndSequenceMixingStation
    Implements IDisposable

#Region "fields"
    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private Indigo As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String
#End Region

#Region "Builder"
    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tag As String)
        _tagForm = tag
        Indigo = SessionValues.Instance
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene la secuencia numerica asignada al formulario
    ''' </summary>
    ''' <returns>Secuencia numerica</returns>
    Public Async Function GetSequence() As Task(Of Domain.Entities.MixingStationSequence)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetSequenceByIdFormAsync(Me._tagForm)
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por el id de la configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia numerica</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Async Function GetNumericSequenceGroup(ByVal id As Int32) As Task(Of List(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetNumericSequenceGroupByIdAsync(id)
    End Function

    ''' <summary>
    ''' Funcion para guardar el registro bloqueado
    ''' </summary>
    ''' <param name="record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecord(ByVal record As BlockRecordMixingStation) As Task(Of ActionResult(Of BlockRecordMixingStation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveBlockRecordMixingStationAsync(record)
    End Function

    ''' <summary>
    ''' Funcion para eliminar el registro bloqueado
    ''' </summary>
    ''' <param name="record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecord(ByVal record As BlockRecordMixingStation) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.DeleteBlockRecordMixingStationAsync(record)
    End Function

    ''' <summary>
    ''' Obtener registro bloqueado
    ''' </summary>
    ''' <param name="idForm"></param>
    ''' <param name="idRecord"></param>
    ''' <returns></returns>
    Public Async Function GetBlockRecord(ByVal idForm As String, ByVal idRecord As String) As Task(Of BlockRecordMixingStation)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetBlockRecordMixingStationByIdformAndIdRecordAsync(idForm, idRecord)
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
