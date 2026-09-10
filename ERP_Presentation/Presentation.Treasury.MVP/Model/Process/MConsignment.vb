'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 07-10-2014
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

Public Class MConsignment
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
    ''' Obtiene un registro de consignacion o traslado por id
    ''' </summary>
    Public Async Function GetConsignmentTransferById(ByVal Id As Integer, Optional tracking As Boolean = False) As Task(Of Consignment)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetConsignmentTransferByIdAsync(Id, tracking)
    End Function

    ''' <summary>
    ''' Obtiene un registro de consignacion o traslado por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetConsignmentTransfer(ByVal code As String, Optional tracking As Boolean = False) As Task(Of ActionResult(Of Consignment))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetConsignmentTransferAsync(code, Me._indigoSessionValues.AuditMessageWcf, tracking)
    End Function

    ''' <summary>
    ''' Guarda una consignacion / traslado
    ''' </summary>
    Public Async Function SaveConsignmentTransfer(ByVal consignmentTransfer As Consignment, ByVal withConfirm As Boolean, idSequence As Long) As Task(Of ActionResult(Of Consignment))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveConsignmentTransferAsync(consignmentTransfer, withConfirm, idSequence, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' confirma una consignacion / traslado
    ''' </summary>
    Public Async Function ConfirmConsignmentTransfer(ByVal consignmentTransferId As Integer) As Task(Of ActionResult(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ConfirmConsignmentTransferAsync(consignmentTransferId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Consulta EL TRM de las monedas origne vs destino
    ''' </summary>
    ''' <param name="FromCurrencyId"></param>
    ''' <param name="ToCurrencyId"></param>
    ''' <returns></returns>
    Public Async Function GetTRMbyCurrencyId(ToCurrencyId As Integer, FromCurrencyId As Integer, Optional DateTrm As Date? = Nothing) As Task(Of ActionResult(Of TRM))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetTRMbyCurrencyIdAsync(ToCurrencyId, FromCurrencyId, Me._indigoSessionValues, DateTrm, Nothing)
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