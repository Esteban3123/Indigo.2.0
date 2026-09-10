'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 20-01-2016
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
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo

#End Region

Public Class PEquipmentEntry

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IEquipmentEntry

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As New SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IEquipmentEntry)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        Me.View = iview
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Sub New()
        ' TODO: Complete member initialization 
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequenceFixedAsset(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene un articulo por id
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetFixedAssetItemById(Id As Integer, indigo As SessionValues) As XPCollection
        Return XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.GetFixedAssetItemById(Id)
    End Function

  
#End Region

End Class
