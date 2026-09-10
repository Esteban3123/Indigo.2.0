'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Rafael Patiño
' Created          : 10-04-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports  Domain.Entities
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PRadicateInvoice

#Region "variables y constructor "
    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz
    ''' </summary>
    Dim View As IRadicateInvoice

    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase MObjectionReception
    ''' </summary>
    Dim MRadicateInvoice As MRadicateInvoice

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IRadicateInvoice)
        If iview Is Nothing Then
            Throw New ArgumentNullException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
            Me.Indigo = SessionValues.Instance
            Me.MRadicateInvoice = New MRadicateInvoice("509")
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para Cargar el GridLookUpEdit
    ''' </summary>
    Public Async Sub Initializes()
        View.DataSourceBranch = Await MRadicateInvoice.GetBranchAll()
    End Sub

    ''' <summary>
    ''' obtiene la secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetSequense()
        Using model As New MRadicateInvoice(CStr(Me.View.MyTag))
            Me.View.Sequense = Await model.GetSequense()
            If Me.View.Sequense?.PortfolioSequenceDetail.Any Then
                Me.View.IdSequence = Me.View.Sequense?.PortfolioSequenceDetail(0)?.Id
            End If
        End Using
    End Sub

#End Region

End Class
