'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 16-04-2013
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
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources

#End Region
''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal de Unidades de Negocio
''' </summary>
Public Class PBranchOffice

#Region "Fields"

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IBusiness Unit
    ''' </summary>
    Private _view As IBranchOffice
    ''' <summary>
    ''' Variable que se utilizapa para tratar las unidades de negocio como un Objeto
    ''' </summary>
    Private _businessUnit As Object
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Private _indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor de la clase presentador en el frontal de unidades de negocio
    ''' </summary>
    ''' <param name="view">Vista de las unidades de negocio</param>
    Public Sub New(ByRef view As IBranchOffice)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub
    ''' <summary>
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Sub Initializes()
        Using Model As New MCompany(MCompany.TAG)
            _view.Company = Model.ListAllCompany
        End Using
    End Sub
#End Region

End Class
