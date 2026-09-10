'***********************************************************************
' Assembly         : Presentacion.payrol.MVP
' Author           : Rafael Eduardo patiño 
' Created          : 07-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PAgreements


#Region "variables y constructor "
    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz
    ''' </summary>
    Dim View As IAgreements
    ''' <summary>
    ''' Variable que se utilizapa para tratar al Responsable como un Objeto
    ''' </summary>
    Dim Responsible As Object
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IAgreements)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
        View.StatusAgreements = True
    End Sub
#End Region


#Region "Metodos"
    Public Async Sub LoadDataAsync()
        Using model As New MAgreements("595")
            Me.View.AsyncLoader(True)
            'Me.View.ListEmployee = Await modelBusqueda.ConsultarEntidades(eDataSource.ListEmployeeWithActiveContract)
            Me.View.ListCompany = Await model.ListAllCompany()
            '  Me.View.ListKindsAgreements = Await model.ListKindsAgreements()
            Me.View.ListConcept = Await model.ListAllConceptAsync()
            Me.View.AsyncLoader(False)
        End Using

        Using modelBusqueda As New MBusqueda
            'Dim EmployeeDataSource = modelBusqueda.ConsultarEntidades(eDataSource.ListEmployeeWithActiveContract)
            Me.View.ListEmployee = modelBusqueda.ConsultarEntidades(eDataSource.ListEmployeeWithActiveContract)
        End Using
    End Sub

    Public Async Function LoadKindsAgreements() As Task
        Using model As New MAgreements("595")
            Me.View.ListKindsAgreements = Await model.ListKindsAgreements()
        End Using
    End Function

    Public Sub InitializeAccountReceivableAccountingXPO(thirdPartyId As Integer)
        If View.ListAccountReceivableAccountingXPO Is Nothing Then
            View.ListAccountReceivableAccountingXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.ListAccountReceivableAccountingByThirdParty(thirdPartyId)
        End If
    End Sub
    ''' <summary>
    ''' Obtiene los datos de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadPayrollSettings() As PayrollSettingsXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollSettingsXpo).FirstOrDefault()
    End Function

#End Region
End Class

''' <summary>
''' Enumeracion del tipo de liquidacion
''' </summary>
''' <remarks></remarks>
Public Enum EnumLiquidationType
    ''' <summary>
    ''' Fijo
    ''' </summary>
    ''' <remarks></remarks>
    eFixed = 1
    ''' <summary>
    ''' variable
    ''' </summary>
    ''' <remarks></remarks>
    eVariable = 2
End Enum
''' <summary>
''' Enumeracion de tipo de plazo
''' </summary>
''' <remarks></remarks>
Public Enum EnumTermType
    ''' <summary>
    ''' Fijo
    ''' </summary>
    ''' <remarks></remarks>
    eFixed = 1
    ''' <summary>
    ''' Variable
    ''' </summary>
    ''' <remarks></remarks>
    eVariable = 2
End Enum
''' <summary>
''' Enumeracion del estado del convenio
''' </summary>
''' <remarks></remarks>
Public Enum EnumStateAgreements
    ''' <summary>
    ''' Sin confirmar
    ''' </summary>
    ''' <remarks></remarks>
    eUnconfirmed = 1
    ''' <summary>
    ''' confirmada
    ''' </summary>
    ''' <remarks></remarks>
    eConfirmed = 2
    ''' <summary>
    ''' Suspendida
    ''' </summary>
    ''' <remarks></remarks>
    eSuspended = 3
    ''' <summary>
    ''' Finalizada
    ''' </summary>
    ''' <remarks></remarks>
    eFinished = 4
    ''' <summary>
    ''' Invalidada
    ''' </summary>
    ''' <remarks></remarks>
    einvalidated = 5
End Enum








