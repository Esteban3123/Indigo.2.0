'***********************************************************************
' Assembly         : Application.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/09/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICopyBaseAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda la copia de presupuesto
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveCopyBase(ByVal CopyBase As CopyBase, ByVal audit As AuditMessage) As ActionResult(Of CopyBase)

End Interface
