'***********************************************************************
' Assembly         : DistributedService.Base
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-03-04
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Security
Imports System.Globalization
Imports Application.Base
Imports Infrastructure.CrossCutting.IOC
Imports System.Configuration
#End Region

''' <summary>
''' 	
''' </summary>
Public Class CommonService
    Implements ICommonService

    ''' <summary>
    ''' Gets the custom date.	
    ''' </summary>
    ''' <param name="format">The format.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomDate(format As FormatDate) As String Implements ICommonService.GetCustomDateFormatDate
        Select Case format
            Case FormatDate.Shortdate
                Return DateTime.Now.ToString("d", CultureInfo.CurrentCulture)
            Case FormatDate.Longdate
                Return DateTime.Now.ToString("D", CultureInfo.CurrentCulture)
            Case FormatDate.GeneralShortDatetime
                Return DateTime.Now.ToString("g", CultureInfo.CurrentCulture)
            Case FormatDate.GeneralLongDatetime
                Return DateTime.Now.ToString("G", CultureInfo.CurrentCulture)
            Case FormatDate.FullShortDatetime
                Return DateTime.Now.ToString("f", CultureInfo.CurrentCulture)
            Case FormatDate.FullLongDatetime
                Return DateTime.Now.ToString("F", CultureInfo.CurrentCulture)
            Case Else
                Return DateTime.Now.ToString
        End Select
    End Function

    ''' <summary>
    ''' Gets the custom date.	
    ''' </summary>
    ''' <param name="format">The format.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomDate(format As String) As String Implements ICommonService.GetCustomDateStringFormat
        If String.IsNullOrEmpty(format) = True Then
            Throw New ArgumentNullException("format Vacio")
        End If
        Return DateTime.Now.ToString(format, CultureInfo.CurrentCulture)
    End Function

    ''' <summary>
    ''' Gets the server date.	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetServerDate() As Date Implements ICommonService.GetServerDate
        Return DateTime.Now
    End Function

    ' ''' <summary>
    ' ''' Consulta los campos nulos para una tabla en un esquema especifico
    ' ''' </summary>
    ' ''' <param name="schema">Esquema</param>
    ' ''' <param name="nameTable">Nombre de la tabla</param>
    ' ''' <returns>DataSet con el conjunto de campos que son null</returns>
    ' ''' <remarks></remarks>
    'Public Function GetFieldsNULL(company As String, schema As String, nameTable As String) As DataSet Implements ICommonService.GetFieldsNULL
    '    Dim container = System.Configuration.ConfigurationManager.AppSettings(ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME)
    '    If container Is Nothing OrElse container.ToString().Trim().Equals(String.Empty) Then
    '        Throw New ArgumentNullException("containerSecurity", "El parametro no existe en el archivo de configuración o se encuentra vacío")
    '    End If
    '    Dim commonAdminService As ICommonAdminService = IocFactory.Instance(container).CurrentContainer.Resolve(Of ICommonAdminService)()
    '    Return commonAdminService.GetFieldsNull(ConfigurationFile.COMPANY_CONTAINER_NAME & company.Trim(), schema, nameTable)
    'End Function

    ''' <summary>
    ''' Obtiene la credencial de autenticación usada por los servicios de notificación
    ''' </summary>
    ''' <returns>Credencial</returns>
    Public Function GetNotificationServiceCredential() As CredentialBasic Implements ICommonService.GetNotificationServiceCredential
        Try
            Dim user As String = ConfigurationManager.AppSettings("UserNotificationService")
            Dim passwd As String = ConfigurationManager.AppSettings("PasswdNotificationService")
            Dim credential As New CredentialBasic()
            If user IsNot Nothing AndAlso Not user.Trim().Equals(String.Empty) Then
                credential.User = IndigoRijndael.Decrypt(user.Trim())
            Else
                credential.User = String.Empty

            End If
            If passwd IsNot Nothing AndAlso Not passwd.Trim().Equals(String.Empty) Then
                credential.Passwd = IndigoRijndael.Decrypt(passwd.Trim())
            Else
                credential.Passwd = String.Empty
            End If
            Return credential
        Catch ex As Exception
            Return New CredentialBasic With {.User = String.Empty, .Passwd = String.Empty}
        End Try
    End Function

End Class
