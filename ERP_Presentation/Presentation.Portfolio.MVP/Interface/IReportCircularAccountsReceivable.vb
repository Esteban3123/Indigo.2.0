Imports DevExpress.Xpo

Public Interface IReportCircularAccountsReceivable

    ''' <summary>
    ''' Obtiene o establece los ids de las entidad administradora seleccionadas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HealthAdministratorIds As String

#Region "XPO"

    ''' <summary>
    ''' Establece el datasource de las entidades administradoras de salud
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HealthAdministratorXpo As XPCollection(Of Infrastructure.Data.Xpo.ContractRepository.HealthAdministratorXpo)

#End Region

End Interface
