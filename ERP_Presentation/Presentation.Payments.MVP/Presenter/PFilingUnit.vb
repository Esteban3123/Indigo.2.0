'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/03/2015
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
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports Presentation.Payroll.MVP

#End Region

Public Class PFilingUnit

#Region "Variables and Constructors"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IFilingUnit

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IFilingUnit)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    Public Async Function GetSequense() As Task
        Using model As New MBlockRecordAndSequensePayments(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    Public Sub InitializeStruct()
        Using model As New MBusqueda
            Me.View.StructXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFilingUnitByStatus, True)
        End Using
    End Sub

    Public Sub InitializeUsers()
        Using model As New MFunctionalUnit("")
            Me.View.UserXpo = model.ListAllUser(Indigo.SecurityContainer)
        End Using
    End Sub

#End Region

End Class
