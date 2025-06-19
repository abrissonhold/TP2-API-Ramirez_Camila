# Backend - API Gestión de Proyectos UNAJ
Este es el backend para un sistema de gestión y aprobación de propuestas de proyectos desarrollado en .NET 8 con Entity Framework Core. Expone una API RESTful que permite crear, listar y aprobar propuestas de proyectos siguiendo una lógica de negocio basada en reglas de aprobación.

## Tecnologías
- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- Swagger (OpenAPI)
- Arquitectura por capas

## Funcionalidades
- Crear proyectos con asignación automática de pasos de aprobación.
- Obtener detalle completo de un proyecto (propuesta + pasos).
- Filtrar proyectos por título, estado y solicitante.
- Tomar decisiones de aprobación/rechazo/observación por paso.
- Regla de negocio de aprobación basada en tipo, área, monto y rol del aprobador.
- Semillas de datos para usuarios, roles, tipos, áreas y reglas.
  
## Frontend relacionado
Este backend es consumido por un frontend en HTML, CSS y JS:
![Link al front](https://github.com/abrissonhold/TP3-FRONT-Ramirez_Camila)
