'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Jorge Leonardo Vernaza
' Created          : 09-04-2013
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
Public Class PObjectionsReception

#Region "variables y constructor "
    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz
    ''' </summary>
    Dim View As IObjectionsReception
    ''' <summary>
    ''' Variable que se utilizapa para tratar a la cabecera como un Objeto
    ''' </summary>
    Dim ObjectionsReceptionC As Object
    ''' <summary>
    ''' Variable que se utilizapa para tratar al detalle como un Objeto
    ''' </summary>
    Dim GlosaObjectionsReceptionD As List(Of Object)
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase MObjectionReception
    ''' </summary>
    Dim MObjection As MObjectionsReception

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IObjectionsReception)
        If iview Is Nothing Then
            Throw New ArgumentNullException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
            Me.Indigo = SessionValues.Instance
            Me.MObjection = New MObjectionsReception("508")
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para Cargar el GridLookUpEdit
    ''' </summary>
    Public Async Sub Initializes()
        View.DataSourceBranch = Await MObjection.GetBranchAll()
    End Sub

    ''' <summary>
    ''' obtiene la secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetSequense()
        Using model As New MObjectionsReception(CStr("509")) 'tag de radicacion de cuentas, con el que quedo registrado la secuencia numerica para Doc. de reclasificacion
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Guarda un oficio
    ''' </summary>
    ''' <param name="Record">cabecera de oficio</param>
    ''' <param name="ListGlosaObjectionsReceptionD">lista de detalle</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function saveObjectionsReception(ByVal Record As GlosaObjectionsReceptionC, ByVal ListGlosaObjectionsReceptionD As List(Of GlosaObjectionsReceptionD)) As Task(Of ActionResult)
        Dim resultObjC As ActionResult = Await MObjection.SaveObjectionsReception(Record)
        Dim resultObjD As New ActionResult
        If resultObjC.StateResult = True Then
            Me.View.Consecutive = resultObjC.MessageResult(1).ToString
            Dim withoutreferencelistD As New List(Of GlosaObjectionsReceptionD)
            withoutreferencelistD.AddRange(ListGlosaObjectionsReceptionD)
            resultObjD = Await Me.saveObjectionsReceptionPersist(resultObjC.MessageResult(0), Record, withoutreferencelistD)
        End If
        Return resultObjC
    End Function
    ''' <summary>
    ''' Metodo Asincrono para guardar un oficio 
    ''' </summary>
    ''' <param name="Record">el nuevo registro</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function saveObjectionsReceptionPersist(ByVal ObjCId As String, ByVal Record As GlosaObjectionsReceptionC, ByVal ListGlosaObjectionsReceptionD As List(Of GlosaObjectionsReceptionD)) As Task(Of ActionResult)
        Dim resultObjD As New ActionResult
        'actualizamos el id de la cabecera y se guarda detalle por detalle actualizando la vista
        For i As Integer = 0 To ListGlosaObjectionsReceptionD.Count - 1
            ListGlosaObjectionsReceptionD(i).GlosaObjectionsReceptionCId = ObjCId
            'verifico que la factura se pueda guardar es decir que no vaya estar radicada en otro oficio
            If ListGlosaObjectionsReceptionD(i).ChangeTracker.State = ObjectState.Added Then
                View.ChangeStade(Stades.Procesando, i)
                If Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                    resultObjD = Await MObjection.SaveObjectionsReceptionDAndPersist(Record.RadicatedConsecutive, Me.View.NameContainer, ListGlosaObjectionsReceptionD(i))
                Else
                    resultObjD = Await MObjection.SaveObjectionsReceptionDAndPersist(Record.RadicatedConsecutive, String.Empty, ListGlosaObjectionsReceptionD(i))
                End If
                'si la factura se guardo correctamente procedo actualizar la rejilla
                If resultObjD.StateResult = True Then
                    View.ChangeStade(Stades.Guardado, i)
                    'variable tipo detalle que se cargara con un detalle ya persistido
                    Dim _tmpObjectionReceptionD As GlosaObjectionsReceptionD
                    'cargamos el detalle ya persistido, por medio del numero de factura
                    '_tmpObjectionReceptionD = MObjection.GetReceptionsObjectionD(ListGlosaObjectionsReceptionD(i).InvoiceNumber)
                    _tmpObjectionReceptionD = Await MObjection.GetReceptionsObjectionD(ListGlosaObjectionsReceptionD(i).GlosaPortfolioGlosada.InvoiceNumber, ObjCId)
                    'se envia el numero de factura y detalle persistido para la activacion en la rejilla y actualizacion del item detalle
                    View.ActiveRecord(ListGlosaObjectionsReceptionD(i).GlosaPortfolioGlosada.InvoiceNumber, _tmpObjectionReceptionD) = ListGlosaObjectionsReceptionD(i).GlosaPortfolioGlosada.InvoiceNumber
                Else
                    View.ChangeStade(Stades.SinGuardar, i)
                End If
            End If
        Next
        View.CompletedPersist = True
        Return resultObjD
    End Function

#End Region

End Class
