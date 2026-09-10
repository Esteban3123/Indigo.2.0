'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 17-04-2013
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
Imports Presentation.Common.MVP

#End Region
''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal de Fondos
''' </summary>
Public Class PFunds
#Region "Fields"

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IFounds
    ''' </summary>
    Private _view As IFunds
    ''' <summary>
    ''' Variable que se utilizapa para tratar los fondos como un Objeto
    ''' </summary>
    Private _founds As Object
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Private _indigo As SessionValues = SessionValues.Instance

#End Region
#Region "Builders"

    ''' <summary>
    ''' Constructor de la clase presentador en el frontal de Fondos
    ''' </summary>
    ''' <param name="view">Vista de las profesiones</param>
    Public Sub New(ByRef view As IFunds)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub

    Public Async Function initializes() As Task
        Using Model As New MThirdParty(MThirdParty.TAG)
            _view.DataSourceThirdParty = Await Model.ListAllThirdPartyAsync()
        End Using
    End Function

    Public Async Sub GetSequence()
        Using model As New MBlockRecordAndSequensePayroll(_view.MyTag)
            Me._view.Sequence = Await model.GetSequense()
        End Using
    End Sub
#End Region
End Class
