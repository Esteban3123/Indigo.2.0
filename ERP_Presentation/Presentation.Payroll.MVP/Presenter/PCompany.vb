'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 25-04-2013
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
''' Esta presentador captura toda la logica aplicada en el frontal de Empresas
''' </summary>
Public Class PCompany
#Region "Fields"

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz ICompany
    ''' </summary>
    Private _view As ICompany
    ''' <summary>
    ''' Variable que se utilizapa para tratar las empresas como un Objeto
    ''' </summary>
    Private _company As Object
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Private _indigo As SessionValues = SessionValues.Instance

#End Region

    Public Sub LoadListThirdParty()
        Using Model As New MForeclousure("2030")
            Me._view.ListThirdParty = Model.ListAllThirdPartyXpo()
        End Using
    End Sub
#Region "Builders"

    ''' <summary>
    ''' Constructor de la clase presentador en el frontal Empresas
    ''' </summary>
    ''' <param name="view">Vista de las Empresas</param>
    Public Sub New(ByRef view As ICompany)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub

#End Region
End Class
