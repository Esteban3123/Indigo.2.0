'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 25-09-2014
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

Public Class MVoucherTransactionCrossing
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
    ''' Obtiene un registro de cruce de cuentas por ir
    ''' </summary>
    Public Async Function GetCrossingAccountById(ByVal Id As Integer, Optional tracking As Boolean = False) As Task(Of CrossingAccount)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCrossingAccountByIdAsync(Id, tracking)
    End Function

    ''' <summary>
    ''' Obtiene un registro de cruce de cuentas por codigo
    ''' </summary>
    Public Async Function GetCrossingAccount(ByVal code As String, Optional tracking As Boolean = False) As Task(Of ActionResult(Of CrossingAccount))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCrossingAccountAsync(code, Me._indigoSessionValues.AuditMessageWcf, tracking)
    End Function

    ''' <summary>
    ''' Guardar Cruce de Cuentas
    ''' </summary>
    Public Async Function SaveCrossingAccount(ByVal crossingAccount As CrossingAccount, ByVal withConfirm As Boolean, idSequence As Long) As Task(Of ActionResult(Of CrossingAccount))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveCrossingAccountAsync(crossingAccount, withConfirm, Me._indigoSessionValues.AuditMessageWcf, idSequence)
    End Function

    ''' <summary>
    ''' Comfirma el cruce de cuentas
    ''' </summary>
    Public Async Function ConfirmCrossingAccount(crossingAccountId As Integer) As Task(Of ActionResult(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ConfirmCrossingAccountAsync(crossingAccountId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un cruce de cuentas
    ''' </summary>
    Public Async Function DeleteCrossingAccount(ByVal crossingAccount As CrossingAccount) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.DeleteCrossingAccountAsync(crossingAccount, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene el detalle del cruce de cuentas de CxP por id
    ''' </summary>
    Public Async Function GetCrossingAccountDetailCxPById(Id As Integer, Optional tracking As Boolean = False) As Task(Of CrossingAccountDetailCxP)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCrossingAccountDetailCxPByIdAsync(Id, tracking)
    End Function

    ''' <summary>
    ''' Lista los detalles de cruces de cuentas de CxP por el id de cruce de cuenta
    ''' </summary>
    Public Async Function ListCrossingAccountDetailCxPByCrossingAccountId(crossingAccountId As Integer, Optional tracking As Boolean = False) As Task(Of List(Of CrossingAccountDetailCxP))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListCrossingAccountDetailCxPByCrossingAccountIdAsync(crossingAccountId, tracking)
    End Function

    ''' <summary>
    ''' Obtiene el detalle del cruce de cuentas de CxC por id
    ''' </summary>
    Public Async Function GetCrossingAccountDetailCxCById(Id As Integer, Optional tracking As Boolean = False) As Task(Of CrossingAccountDetailCxC)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCrossingAccountDetailCxCByIdAsync(Id, tracking)
    End Function

    ''' <summary>
    ''' Lista los detalles de cruces de cuentas de CxC por el id de cruce de cuenta
    ''' </summary>
    Public Async Function ListCrossingAccountDetailCxCByCrossingAccountId(crossingAccountId As Integer, Optional tracking As Boolean = False) As Task(Of List(Of CrossingAccountDetailCxC))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListCrossingAccountDetailCxCByCrossingAccountIdAsync(crossingAccountId, tracking)
    End Function


    ''' <summary>
    '''  establece las cuentas por cobrar del copiar y pegar
    ''' </summary>
    ''' <param name="data">Listado que se va a procesar</param>
    ''' <param name="idThirdPaty"></param>
    ''' <param name="crossingType">1-Mismo Tercero, 2-Diferente Tercero</param>
    ''' <param name="processType">1 - CxP, 2 - CxC </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SetDocumentsCrossingCopyPaste(data As List(Of List(Of String)), idThirdPaty As Integer, crossingType As Integer, processType As Integer) As Task(Of ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SetDocumentsCrossingCopyPasteAsync(data, idThirdPaty, crossingType, processType)
    End Function

    ''' <summary>
    '''  establece las cuentas por cobrar del copiar y pegar
    ''' </summary>
    ''' <param name="data">Listado que se va a procesar</param>
    ''' <param name="idThirdPaty"></param>
    ''' <param name="crossingType">1-Mismo Tercero, 2-Diferente Tercero</param>
    ''' <param name="processType">1 - CxP, 2 - CxC </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SetDocumentsCrossingImportFile(data As List(Of ImportFileRow), idThirdPaty As Integer, crossingType As Integer, processType As Integer) As Task(Of ActionResult(Of List(Of CrossingAccountDetailCxP), List(Of CrossingAccountDetailCxC)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SetDocumentsCrossingImportFileAsync(data, idThirdPaty, crossingType, processType)
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