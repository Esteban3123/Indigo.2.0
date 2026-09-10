'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 13-08-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Treasury
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class TreasuryService

    ''' <summary>
    ''' Elimina un registro de control de los documentos de tesoreria
    ''' </summary>
    ''' <param name="treasuryControl"></param>
    ''' <returns></returns>
    Public Function DeleteTreasuryControl(treasuryControl As TreasuryControl, audit As AuditMessage) As ActionResult Implements ITreasuryServiceTreasuryControl.DeleteTreasuryControl
        Using service As ITreasuryControlAdminService = Container.Current.Resolve(Of ITreasuryControlAdminService)()
            Return service.DeleteTreasuryControl(treasuryControl, audit)
        End Using
        'Return Me._treasuryControlAdminService.DeleteTreasuryControl(treasuryControl, audit)
    End Function

    ''' <summary>
    ''' Obtiene un registro de control de los documentos de tesoreria por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetTreasuryControlById(Id As Integer) As TreasuryControl Implements ITreasuryServiceTreasuryControl.GetTreasuryControlById
        Using service As ITreasuryControlAdminService = Container.Current.Resolve(Of ITreasuryControlAdminService)()
            Return service.GetTreasuryControlById(Id)
        End Using
        'Return Me._treasuryControlAdminService.GetTreasuryControlById(Id)
    End Function

    ''' <summary>
    ''' Guarda un registro de control de los documentos de tesoreria
    ''' </summary>
    ''' <param name="treasuryControl"></param>
    ''' <returns></returns>
    Public Function SaveTreasuryControl(treasuryControl As TreasuryControl, audit As AuditMessage) As ActionResult(Of TreasuryControl) Implements ITreasuryServiceTreasuryControl.SaveTreasuryControl
        Using service As ITreasuryControlAdminService = Container.Current.Resolve(Of ITreasuryControlAdminService)()
            Return service.SaveTreasuryControl(treasuryControl, audit)
        End Using
        'Return Me._treasuryControlAdminService.SaveTreasuryControl(treasuryControl, audit)
    End Function
End Class