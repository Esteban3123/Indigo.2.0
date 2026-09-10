Imports System.ServiceModel
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.CloudAgent

Public Class MPatientDeparture
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
    ''' lista los centros de atencion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCentersHIS() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListCentersPatientDepartureHIS(_indigoSessionValues.AuditMessageWcf.CodeUser)
    End Function

    ''' <summary>
    ''' Lista las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListUnitFunctionalHIS(CareCenter As String) As XPCollection(Of ViewUnitFunctionalHis)
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListUnitFunctionalPatientDepartureHIS(_indigoSessionValues.AuditMessageWcf.CodeUser, CareCenter)
    End Function
    ''' <summary>
    ''' lista los pacientes egresados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPatientDepartureHIS(CenterCareCode As String, UnitFunctionalCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListPatientDepartureHIS(CenterCareCode, UnitFunctionalCode)
    End Function

    Public Async Function SP_ListCareCenterHis() As Task(Of ActionResult(Of List(Of SP_ListCareCenterHis_Result)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SP_ListCareCenterHisAsync(_indigoSessionValues.UserIndigo, _indigoSessionValues.UserGroup, _indigoSessionValues.HisContainer)
    End Function

    Public Function SP_ListCareCenterHisNotAsync() As ActionResult(Of List(Of SP_ListCareCenterHis_Result))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SP_ListCareCenterHis(_indigoSessionValues.UserIndigo, _indigoSessionValues.UserGroup, _indigoSessionValues.HisContainer)
    End Function

    Public Async Function SP_ListFunctionalUnitHis(CareCenterCode As String) As Task(Of ActionResult(Of List(Of SP_ListFunctionalUnitHis_Result)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SP_ListFunctionalUnitHisAsync(CareCenterCode, _indigoSessionValues.UserIndigo, _indigoSessionValues.UserGroup, _indigoSessionValues.HisContainer)
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
