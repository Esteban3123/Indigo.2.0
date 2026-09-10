Imports System.Text
Imports Domain.Base.Entities

Public Interface IAutoliquidationDomain
    Inherits IDisposable

    ''' <summary>
    ''' Funcion para generar el archivo del la PILA
    ''' </summary>
    ''' <param name="company">Compañia</param>
    ''' <param name="workCenter">Centro de trabajo</param>
    ''' <param name="periodLiquidation">Periodo de liquidacion</param>
    ''' <param name="isCorrection">Es correccion</param>
    ''' <param name="dateLiquidation">Fecha de liquidacion</param>
    ''' <param name="numberTemplate">Numero de plantilla</param>
    ''' <param name="listLiquidation">Lista de liquidacion</param>
    ''' <returns>Retorna un StringBuilder el cual contiene el archivo que se va a escribir</returns>
    ''' <remarks></remarks>
    Function GenerateArchive(company As Company, workCenter As WorkCenter, periodLiquidation As String, isCorrection As Boolean,
                                    dateLiquidation As Nullable(Of Date), numberTemplate As String, listLiquidation As List(Of Liquidation)) As ActionMessageResult(Of StringBuilder)

    ''' <summary>
    ''' Funcion para generar el digito de verificacion
    ''' </summary>
    ''' <param name="nit">Nit del tercero</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Calculate_VerificationCode(ByVal nit As String) As String

End Interface
