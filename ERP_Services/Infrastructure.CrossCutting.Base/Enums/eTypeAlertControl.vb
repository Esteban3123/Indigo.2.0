Imports System.ComponentModel
Imports System.Runtime.Serialization
''' <summary>
''' Enumeracion para establecer los tipos de control de alerta
''' </summary>
<DataContract()>
Public Enum eTypeAlertControl
    ''' <summary>
    ''' Indica que el tipo de rol es global
    ''' </summary>
    <EnumMember>
    <Description("Alert Windows")>
    AlertWindows = 1
    ''' <summary>
    ''' Indica que el tipo de rol es por tenant
    ''' </summary>
    <EnumMember>
    <Description("Toast Notification")>
    ToastNotification = 2
End Enum
