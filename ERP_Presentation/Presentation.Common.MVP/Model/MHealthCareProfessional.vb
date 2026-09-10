'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 12-02-2015
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
Imports Presentation.Security.MVP
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Domain.Crystal.Entities
Imports Presentation.Controls.MVP

#End Region
Public Class MHealthCareProfessional
    Inherits ModelBase
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
        MyBase.New(tag)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un profesional por código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetHealthProfessionalByCodeAsync(Code As String) As Task(Of ActionResult(Of HealthProfessionalModel))
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetHealthProfessionalByCodeAsync(Code)
    End Function

    ''' <summary>
    ''' Lista Todos Los Profesionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthProfessionals() As XPInstantFeedbackSource
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListHealthCareProfessional()
    End Function

    ''' <summary>
    ''' Obtiene una especialidad Por Código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetSpecialityByCode(Code As String) As Task(Of ActionResult(Of INESPECIA))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetSpecialityByCodeAsync(Code)
    End Function

    ''' <summary>
    ''' Obtiene una especialidad Por Código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetUserHIS(Code As String) As Task(Of ActionResult(Of SEGusuaru))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetUserHISAsync(Code)
    End Function

    ''' <summary>
    ''' Guarda el profesional
    ''' </summary>
    ''' <param name="professional"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveHealthProfessionalAsync(professional As HealthProfessionalModel, ListHealthProfessionalContract As List(Of Domain.Entities.HealthProfessionalContract), ListDeleteHealthProfessionalContract As List(Of Domain.Entities.HealthProfessionalContract)) As Task(Of ActionResult(Of HealthProfessionalModel))
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.SaveHealthProfessionalAsync(professional, ListHealthProfessionalContract, ListDeleteHealthProfessionalContract, Me._indigoSessionValues.AuditMessageWcf)
    End Function


    ''' <summary>
    ''' Elimina un profesional
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteProfessional(Code As String) As Task(Of ActionResult)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.DeleteProfessionalAsync(Code)
    End Function

    ''' <summary>
    ''' Cambia el estado del profesional
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function UpdateStatusProfessionalHealthAsync(Code As String, Status As Boolean) As Task(Of ActionResult(Of HealthProfessionalModel))
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.UpdateStatusProfessionalHealthAsync(Code, Status)
    End Function

    ''' <summary>
    ''' Obtiene el listado de detalles de contratos del medico
    ''' </summary>
    ''' <param name="healthProfessionalCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetListHealthProfessionalContractByHealthProfessionalCode(healthProfessionalCode As String) As Task(Of ActionResult(Of List(Of HealthProfessionalContract)))
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetListHealthProfessionalContractByHealthProfessionalCodeAsync(healthProfessionalCode)
    End Function

    ''' <summary>
    ''' Obtener las especialidades
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSpecialties() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListSpecialties(True)
    End Function


    ''' <summary>
    ''' Obtener los contratos de los profesionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMedicalFeesContract() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MedicalFeesService.ListMedicalFeesContractByStatus(True)
    End Function

    ''' <summary>
    ''' Obtiene el listado de proveedor lineas de distribucion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSupplierDistributionLine() As XPInstantFeedbackSource
        Dim model As New MBusqueda
        Return CType(model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSuppliersDistributionLines), DevExpress.Xpo.XPInstantFeedbackSource)
    End Function

    Public Function ListIdentificationType() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CrystalService.ListADTIPOIDENTIFICAxpoActive()
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class



