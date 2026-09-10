Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities

Public Interface IInterfaceNET
    Inherits IDisposable

    Function RadicateObjection(ByVal IndigoEmpresa As String, ByVal NumeroGlosa As String, ByVal factura As String, ByVal Tercero As String,
                               ByVal NameContainer As String, ByVal ValorFac As Decimal, ByVal FechaFactura As Integer, ByVal intOpcion As String,
                               ByVal User As String) As InterfaceResult

    Function CreateCreditNote(ByVal NameContainer As String, ByVal Tercero As String, ByVal factura As String, ByVal ValorFac As Decimal, ByVal User As String,
                              ByVal Cuenta As String, ByVal Concepto As String, ByVal CuentaConcepto As String) As InterfaceResult


    Function AcceptanceEAPB(ByVal IndigoEmpresa As String, ByVal NumeroGlosa As String, ByVal factura As String, ByVal Tercero As String,
                           ByVal NameContainer As String, ByVal ValorFac As Decimal, ByVal FechaFactura As Integer, ByVal intOpcion As String,
                           ByVal User As String, Optional ByVal EjecutaAceptacionEAPBUnicaTransaccion As Boolean = False) As InterfaceResult

    Function CreateDebitNote(ByVal NameContainer As String, ByVal Tercero As String, ByVal factura As String, ByVal ValorFac As Decimal, ByVal User As String,
                             ByVal Cuenta As String, ByVal Concepto As String, ByVal CuentaConcepto As String, ByVal Reiterated As Boolean, Optional ByVal EjecutaAceptacionEAPBUnicaTransaccion As Boolean = False) As InterfaceResult


    Function AcceptanceIPS(ByVal IndigoEmpresa As String, ByVal NumeroGlosa As String, ByVal factura As String, ByVal Tercero As String, ByVal NameContainer As String,
                           ByVal ValorFac As Decimal, ByVal FechaFactura As Integer, ByVal intOpcion As String, ByVal User As String, ByVal AfectaServicio As Boolean,
                           ByVal Modulo As String, Optional BanderaEjecutaAceptacionEAPB As Boolean = False, Optional VAlorAceptadoEAPB As Decimal = 0) As InterfaceResult


    Function TransferJuridical(IndigoEmpresa As String, NumeroGlosa As String, factura As String, Tercero As String, NameContainer As String, ValorFac As Decimal,
                               FechaFactura As Integer, intOpcion As String, User As String) As InterfaceResult

    ''' <summary>
    ''' Funcion para cargar saldo de fatura de ERP
    ''' </summary>
    ''' <param name="NumberInvoice"></param>
    ''' <param name="NameContainer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadBalanceInvoice(NumberInvoice As String, NameContainer As String) As Decimal


    Function BalanceReiterated(IndigoEmpresa As String, NumeroGlosa As String, factura As String, Tercero As String, NameContainer As String, ValorFac As Decimal, FechaFactura As Integer, intOpcion As String, User As String, Optional ByVal EjecutaAceptacionEAPBUnicaTransaccion As Boolean = False) As InterfaceResult


    Function Devolucion(IndigoEmpresa As String, ByVal listRadicated As List(Of GlosaDevolutionsReceptionD), NameContainer As String, intOpcion As String, User As String) As List(Of InterfaceResult)

End Interface
