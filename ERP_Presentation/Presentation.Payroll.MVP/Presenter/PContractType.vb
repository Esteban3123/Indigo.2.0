'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 05-07-2013
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
Imports Presentation.Controls.MVP

#End Region
''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal de tipos de contrato
''' </summary>
Public Class PContractType

#Region "Fields"

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IContractType
    ''' </summary>
    Private _view As IContractType
    ''' <summary>
    ''' Variable que se utilizapa para tratar los centros de estudio como un Objeto
    ''' </summary>
    Private _contracttype As Object
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Private _indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor de la clase presentador en el frontal de tipos de contrato
    ''' </summary>
    ''' <param name="view">Vista de tipos de contrato</param>
    Public Sub New(ByRef view As IContractType)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub

    ''' <summary>
    ''' Metodo para inicializar el grid look up edit de grupos de contratos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Sub Initialize()
        Dim Model As New MBusqueda
        _view.DataSourceJobBondingTypeXPO = Model.ConsultarEntidades(eDataSource.JobBondingType)
    End Sub

#End Region
End Class
