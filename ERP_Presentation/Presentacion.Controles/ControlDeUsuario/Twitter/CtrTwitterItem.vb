'***********************************************************************'
' Assembly         : Presentation.Controls                              '
' Author           : Jorge Leonardo Vernaza                             '
' Created          : 01-11-2012                                         '
'                                                                       '
' Last Modified By :                                                    '
' Last Modified On :                                                    '
' Description      :                                                    '
'                                                                       '
' Copyright        : (c) . All rights reserved.                         '
'***********************************************************************'
#Region "Librerias Importadas"

#End Region
''' <summary>
''' Clase que contiene la funcionalidad del control que muestra cada twitter
''' </summary>
Public Class CtrTwitterItem

#Region "Propiedades"
    ''' <summary>
    ''' Nombre del usuario que envio el twitter.
    ''' </summary>
    Public Property INDUserName As String
        Get
            Return INDlblUserName.Text
        End Get
        Set(value As String)
            INDlblUserName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Foto del usuario que envio el twitter.
    ''' </summary>
    Public Property INDUserPhoto As String
        Get
            Return INDpeUserImage.Tag
        End Get
        Set(value As String)
            INDpeUserImage.Tag = value
        End Set
    End Property

    ''' <summary>
    ''' Mensaje completo del twitter.
    ''' </summary>
    Public Property INDUserMessage As String
        Get
            Return INDlblMessage.Text
        End Get
        Set(value As String)
            INDlblMessage.Text = value
        End Set
    End Property
#End Region

End Class
