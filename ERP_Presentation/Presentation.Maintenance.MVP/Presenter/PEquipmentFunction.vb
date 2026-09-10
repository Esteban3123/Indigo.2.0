'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Juan F. Tamayo
' Created          : 2014-10-23
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base

#End Region

Public Class PEquipmentFunction

#Region "Fields"

    ''' <summary>
    ''' Referencia a la vista del funcional
    ''' </summary>
    Private _view As IEquipmentFunction

    ''' <summary>
    ''' Instancia de los valores de sesión
    ''' </summary>
    Private _sessionValues As SessionValues

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="view">Instancia de la vista del funcional</param>
    Public Sub New(ByVal view As IEquipmentFunction)
        Me._view = view
        Me._sessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al funcional
    ''' </summary>
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequenseMaintenance(Me._view.MyTag)
            Me._view.Sequence = Await model.GetSequense()
        End Using
    End Sub

#End Region

End Class
