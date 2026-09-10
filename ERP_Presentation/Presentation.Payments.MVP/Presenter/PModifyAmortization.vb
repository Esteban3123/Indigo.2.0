'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/03/2015
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

Public Class PModifyAmortization

#Region "Variables and Constructors"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IModifyAmortization

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
    Public Sub New(ByRef iview As IModifyAmortization)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    Public Sub InitializeSupplier()
        Using model As New MBusqueda
            Me.View.SupplierXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSupplierByStatus, True)
        End Using
    End Sub

#End Region

End Class
