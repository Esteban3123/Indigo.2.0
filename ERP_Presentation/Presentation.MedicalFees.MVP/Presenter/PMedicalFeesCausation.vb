'***********************************************************************
' Assembly         : Presentacion.MedicalFees.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/12/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo

#End Region

Public Class PMedicalFeesCausation

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IMedicalFeesCausation

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IMedicalFeesCausation)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    Public Sub InitializeAdmission()
        View.AdmissionXpo = XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListAllAdmissions()
    End Sub

    Public Sub InitializeInvoice(ByVal admissionNumber As String)
        View.InvoiceXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListInvoiceByAdmissionStatus(1, admissionNumber)
    End Sub

    ''' <summary>
    ''' Metodo que consulta los permisos que tiene el formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Permission(ByVal list As Dictionary(Of Integer, String)) As List(Of Integer)
        Dim keyPermissionAddFees = list.ToList.FindAll(Function(item) item.Key = 61).Count
        Dim keyPermissionManualCausation = list.ToList.FindAll(Function(item) item.Key = 62).Count
        Dim keyPermissionChangeHealthProfessional = list.ToList.FindAll(Function(item) item.Key = 65).Count
        Dim keyPermissionDelete = list.ToList.FindAll(Function(item) item.Key = 1).Count
        Dim ListKeys As New List(Of Integer)
        ListKeys.Add(keyPermissionAddFees)
        ListKeys.Add(keyPermissionManualCausation)
        ListKeys.Add(keyPermissionChangeHealthProfessional)
        ListKeys.Add(keyPermissionDelete)
        Return ListKeys
    End Function

#End Region

End Class
