Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Interface

Public Interface IInterfaceValidate


    Function ValidateConcept(ByVal enumeration As eTypeInterface, ByVal ContainerName As String, ByVal CodeConcept As String, ByVal typeconcept As String) As Integer

    Function ValidateAccount(ByVal enumeration As eTypeInterface, ByVal ContainerName As String, ByVal account As String) As Integer

    Function ValidateTableMov(ByVal DateActual As DateTime, ByVal ContainerName As String) As Boolean

    Function ValidateMonthClose(ByVal enumeration As eTypeInterface, ByVal DateActual As DateTime, ByVal ContainerName As String) As Boolean

    Function AuditInterface(ByVal IndigoCompany As String, ByVal intOpcion As String, ByVal NumeroGlosa As String, ByVal factura As String, ByVal accion As String, ByVal Concepto As String, ByVal NumeroConsecutivo As String) As Boolean

End Interface
