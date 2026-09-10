'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán
' Created          : 17-03-2014
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
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Domain.Base.Entities
#End Region

''' <summary>
''' Presentador del frontal Conceptos de Notas
''' </summary>
Public Class POrganizationalStruct

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IOrganizationalStruct

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IOrganizationalStruct)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Initializes the organizational structure.
    ''' </summary>
    Public Sub InitializeOrganizationalStructure()
        Using Model As New MBusqueda
            Me.View.OrganizationalStructureDatasourse = Model.ConsultarEntidades(eDataSource.ListOrganizationalStructure)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the organizational structure with out.
    ''' </summary>
    ''' <param name="p1">The p1.</param>
    Public Sub InitializeOrganizationalStructureWithOut(p1 As String)
        Using Model As New MBusqueda
            Dim filter() As Object = {p1}
            Me.View.OrganizationalStructureDatasourse = Model.ConsultarEntidades(eDataSource.InitializeOrganizationalStructureWithOut, filter)
        End Using
    End Sub

End Class
