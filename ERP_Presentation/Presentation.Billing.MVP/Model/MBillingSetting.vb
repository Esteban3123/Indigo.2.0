'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 18-03-2015
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
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports DevExpress.Data.PLinq
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports System.Dynamic

#End Region

Public Class MBillingSetting
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
    ''' Obtiene un registro de parámetros por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Async Function GetSettingsBillingById(id As Integer, tracking As Boolean) As Task(Of Domain.Entities.SettingsBilling)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetSettingsBillingByIdAsync(id, tracking)
    End Function

    ''' <summary>
    ''' Obtiene un resgistro de parámetros por el id de la unidad operativa
    ''' </summary>
    ''' <param name="IdUnitOperative"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Async Function GetSettingsBillingByIdUnitOperative(IdUnitOperative As Integer, tracking As Boolean) As Task(Of Domain.Entities.SettingsBilling)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetSettingsBillingByIdUnitOperativeAsync(IdUnitOperative, tracking)
    End Function

    Public Function GetSettingsBillingByIdUnitOperativeSimple(IdUnitOperative As Integer, tracking As Boolean) As Domain.Entities.SettingsBilling
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetSettingsBillingByIdUnitOperative(IdUnitOperative, tracking)
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