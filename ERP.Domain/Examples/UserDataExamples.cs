using ERP.Domain.Entities.Auth;

namespace ERP.Domain.Examples
{
    /// <summary>
    /// Ejemplos de uso del campo Data en la entidad User
    /// </summary>
    public static class UserDataExamples
    {
        /// <summary>
        /// Ejemplo 1: Configuraciones de perfil del usuario
        /// </summary>
        public static void SetUserPreferences(User user)
        {
            // Configurar preferencias de idioma
            user.SetAdditionalValue("language", "es-ES");
            
            // Configurar tema de la interfaz
            user.SetAdditionalValue("theme", "dark");
            
            // Configurar zona horaria
            user.SetAdditionalValue("timezone", "America/Mexico_City");
            
            // Configurar notificaciones
            user.SetAdditionalValue("notifications", new
            {
                email = true,
                sms = false,
                push = true
            });
        }

        /// <summary>
        /// Ejemplo 2: Información de perfil adicional
        /// </summary>
        public static void SetProfileInfo(User user)
        {
            // Información personal adicional
            user.SetAdditionalValue("birthDate", "1990-05-15");
            user.SetAdditionalValue("department", "IT");
            user.SetAdditionalValue("position", "Senior Developer");
            
            // Contacto de emergencia
            user.SetAdditionalValue("emergencyContact", new
            {
                name = "María García",
                phone = "+52 555-1234567",
                relationship = "Spouse"
            });
            
            // Habilidades
            user.SetAdditionalValue("skills", new[] { "C#", ".NET", "Angular", "SQL Server" });
        }

        /// <summary>
        /// Ejemplo 3: Configuraciones específicas del sistema
        /// </summary>
        public static void SetSystemConfig(User user)
        {
            // Configuraciones de dashboard
            user.SetAdditionalValue("dashboardLayout", new
            {
                widgets = new[] { "sales", "inventory", "notifications" },
                refreshInterval = 30,
                autoRefresh = true
            });
            
            // Permisos especiales temporales
            user.SetAdditionalValue("temporaryAccess", new
            {
                modules = new[] { "reports", "analytics" },
                expiresAt = DateTime.UtcNow.AddDays(30)
            });
        }

        /// <summary>
        /// Ejemplo 4: Leer datos adicionales
        /// </summary>
        public static void ReadUserData(User user)
        {
            // Leer configuración de idioma
            var language = user.GetAdditionalValue<string>("language");
            
            // Leer configuración de notificaciones
            var notifications = user.GetAdditionalValue<dynamic>("notifications");
            
            // Verificar si existe un valor
            var hasTheme = user.HasAdditionalValue("theme");
            
            // Obtener todas las configuraciones
            var allData = user.AdditionalData;
        }

        /// <summary>
        /// Ejemplo 5: Campos específicos por tipo de usuario (reemplaza las entidades Advisor, Customer, Assistant)
        /// </summary>
        public static void SetUserTypeSpecificData(User user, string userType)
        {
            switch (userType.ToLower())
            {
                case "customer":
                    // Datos que antes estaban en la entidad Customer
                    user.SetAdditionalValue("companyName", "Acme Corp");
                    user.SetAdditionalValue("nitId", "900123456-7");
                    user.SetAdditionalValue("contactPerson", "John Doe");
                    user.SetAdditionalValue("phoneNumber", "+57 300 1234567");
                    user.SetAdditionalValue("address", "Calle 123 #45-67, Bogotá");
                    user.SetAdditionalValue("numberEmployees", 150);
                    user.SetAdditionalValue("industry", "Technology");
                    
                    // Datos adicionales de cliente
                    user.SetAdditionalValue("loyaltyPoints", 1500);
                    user.SetAdditionalValue("preferredPaymentMethod", "credit_card");
                    user.SetAdditionalValue("deliveryInstructions", "Ring doorbell twice");
                    break;
                    
                case "advisor":
                    // Datos que antes estaban en la entidad Advisor
                    user.SetAdditionalValue("lastName", "García");
                    user.SetAdditionalValue("certifications", "PMP, SCRUM Master, ITIL");
                    user.SetAdditionalValue("specialization", "Digital Transformation");
                    
                    // Datos adicionales de advisor
                    user.SetAdditionalValue("yearsExperience", 8);
                    user.SetAdditionalValue("consultingRate", 150.00m);
                    user.SetAdditionalValue("availableHours", "Mon-Fri 9AM-6PM");
                    break;
                    
                case "assistant":
                    // Datos que antes estaban en la entidad Assistant
                    user.SetAdditionalValue("lastName", "Rodríguez");
                    user.SetAdditionalValue("assignedToConsultant", 1);
                    user.SetAdditionalValue("startDate", "2023-03-15");
                    
                    // Datos adicionales de assistant
                    user.SetAdditionalValue("workSchedule", "Part-time");
                    user.SetAdditionalValue("skills", new[] { "Administrative", "Customer Service", "Data Entry" });
                    user.SetAdditionalValue("supervisor", "jane.smith@company.com");
                    break;
                    
                case "employee":
                    user.SetAdditionalValue("employeeId", "EMP001234");
                    user.SetAdditionalValue("hireDate", "2023-01-15");
                    user.SetAdditionalValue("salary", 75000.00m);
                    user.SetAdditionalValue("manager", "john.doe@company.com");
                    break;
                    
                case "supplier":
                    user.SetAdditionalValue("companyTaxId", "RFC123456789");
                    user.SetAdditionalValue("creditLimit", 50000.00m);
                    user.SetAdditionalValue("paymentTerms", "NET30");
                    user.SetAdditionalValue("categories", new[] { "electronics", "accessories" });
                    break;
            }
        }
    }
}
