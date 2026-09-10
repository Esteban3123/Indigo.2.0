Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IFundsLevelRepository
    Inherits IRepository(Of Fund)

    ''' <summary>
    ''' Obtiene un listado de Fondos
    ''' </summary>
    ''' <returns></returns>
    Function ListAllFunds() As List(Of Fund)


    ''' <summary>
    ''' Obtiene un determinado Fondo
    ''' </summary>
    ''' <param name="code">Código del Fondo</param>
    ''' <returns></returns>
    Function GetFunds(ByVal code As String, Optional tracking As Boolean = True)

    ''' <summary>
    ''' Obtiene un tercero atraves del nit
    ''' </summary>
    ''' <param name="nit">Nit del tercero</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetThirdPartyByNit(ByVal nit As String)
    ''' <summary>
    ''' Guarda o Actualiza un fondo y todos sus agregados
    ''' </summary>
    ''' <param name="fund">Fondo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveFund(ByVal fund As Fund) As Boolean

    ''' <summary>
    ''' Función que carga los fondos por Id
    ''' </summary>
    ''' <param name="IdFund"></param>
    ''' <returns>Fund</returns>
    ''' <remarks></remarks>
    Function GetFundsById(IdFund As Integer) As Fund

End Interface
