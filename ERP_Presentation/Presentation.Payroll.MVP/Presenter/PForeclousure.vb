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
Public Class PForeclousure


#Region "variables y constructor "
    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz
    ''' </summary>
    Dim View As IForeclousure
    ''' <summary>
    ''' Variable que se utilizapa para tratar al Responsable como un Objeto
    ''' </summary>
    Dim Responsible As Object
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    Dim Tag As String = "2030"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IForeclousure)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If

    End Sub
#End Region


#Region "Metodos"
    Public Sub LoadDataAsync()
        Using modelBusqueda As New MBusqueda
            Me.View.ListEmployee = modelBusqueda.ConsultarEntidades(eDataSource.ListEmployeeWithActiveContract)
        End Using
    End Sub

    Public Sub LoadListThirdParty()
        Using Model As New MForeclousure(Tag)
            Me.View.ListThirdParty = Model.ListAllThirdPartyXpo()
        End Using
    End Sub

    Public Sub LoadListThirdPartyBeneficiary()
        Using Model As New MForeclousure(Tag)
            Me.View.ListBeneficiaryThirdParty = Model.ListAllThirdPartyXpo()
        End Using
    End Sub

    Public Sub LoadListCity()
        Using Model As New MForeclousure(Tag)
            Me.View.ListCity = Model.ListCity()
        End Using
    End Sub

    Public Async Sub LoadCompany()
        Using Model As New MForeclousure(Tag)
            Dim TmpListCompany = Await Model.ListAllCompany()
            If TmpListCompany IsNot Nothing AndAlso TmpListCompany.Count > 0 Then
                Me.View.ListCompany = TmpListCompany.Where(Function(x) x.ForeclousureType = True).ToList()
            End If
        End Using
    End Sub

    Public Async Sub LoadConcept()
        Using Model As New MForeclousure(Tag)
            Dim TmpListConcept = Await Model.ListAllConceptAsync()
            If TmpListConcept IsNot Nothing AndAlso TmpListConcept.Count > 0 Then
                Me.View.ListConcept = TmpListConcept.Where(Function(x) x.ConceptClass = "053").ToList()
            End If
        End Using
    End Sub

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequensePayroll(Tag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
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









