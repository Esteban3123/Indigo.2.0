'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 18-08-2017
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

Public Class PBlockSchedule

#Region "Fields"

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IFounds
    ''' </summary>
    Private _view As IBlockSchedule
    ''' <summary>
    ''' Variable que se utilizapa para tratar los fondos como un Objeto
    ''' </summary>
    Private _FunctionalUnit As Object
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
    Public Sub New(ByRef view As IBlockSchedule)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub

    Public Async Function initializes() As Task
        'Using Model As New MFunctionalUnit(MThirdParty.TAG)
        '    _view.ListFunctionalUnit = Await Model.ListAllAsync()
        'End Using
    End Function
#End Region

End Class
