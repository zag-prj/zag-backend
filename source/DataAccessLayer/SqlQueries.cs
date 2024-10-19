namespace source.DataAccessLayer
{
    public class SqlQueries
    {
        public const string TryCreateTables = @"
            -- Create client_state table
            CREATE TABLE IF NOT EXISTS client_state (
                id INT PRIMARY KEY,
                repr VARCHAR
            );

            -- Create client table
            CREATE TABLE IF NOT EXISTS client (
                id INT PRIMARY KEY,
                state INT,
                company_name VARCHAR,
                location VARCHAR,
                billing VARCHAR,
                FOREIGN KEY (state) REFERENCES client_state (id)
            );

            -- Create contract_state table
            CREATE TABLE IF NOT EXISTS contract_state (
                id INT PRIMARY KEY,
                repr VARCHAR
            );

            -- Create contract table
            CREATE TABLE IF NOT EXISTS contract (
                id INT PRIMARY KEY,
                state INT,
                client_id INT,
                issued TIMESTAMP,
                until TIMESTAMP,
                price_monthly FLOAT,
                FOREIGN KEY (state) REFERENCES contract_state (id),
                FOREIGN KEY (client_id) REFERENCES client (id)
            );

            -- Create contact table
            CREATE TABLE IF NOT EXISTS contact (
                id INT PRIMARY KEY,
                client_id INT,
                name VARCHAR,
                surname VARCHAR,
                phone_number VARCHAR(10),
                email VARCHAR(80),
                FOREIGN KEY (client_id) REFERENCES client (id)
            );

            -- Create hardware_state table
            CREATE TABLE IF NOT EXISTS hardware_state (
                id INT PRIMARY KEY,
                repr VARCHAR
            );

            -- Create hardware table
            CREATE TABLE IF NOT EXISTS hardware (
                id INT PRIMARY KEY,
                state INT,
                contract_id INT,
                ref_name VARCHAR,
                value FLOAT,
                FOREIGN KEY (state) REFERENCES hardware_state (id),
                FOREIGN KEY (contract_id) REFERENCES contract (id)
            );

            -- Create spec table
            CREATE TABLE IF NOT EXISTS spec (
                id INT PRIMARY KEY,
                value FLOAT,
                name VARCHAR
            );

            -- Create hardware_spec table
            CREATE TABLE IF NOT EXISTS hardware_spec (
                hardware_id INT,
                spec_id INT,
                count INT,
                PRIMARY KEY (hardware_id, spec_id),
                FOREIGN KEY (hardware_id) REFERENCES hardware (id),
                FOREIGN KEY (spec_id) REFERENCES spec (id)
            );

            -- Create technician_state table
            CREATE TABLE IF NOT EXISTS technician_state (
                id INT PRIMARY KEY,
                repr VARCHAR
            );

            -- Create role table
            CREATE TABLE IF NOT EXISTS role (
                id INT PRIMARY KEY,
                title VARCHAR,
                sec_level INT
            );

            -- Create technician table
            CREATE TABLE IF NOT EXISTS technician (
                id INT PRIMARY KEY,
                state INT,
                role INT,
                name VARCHAR,
                surname VARCHAR,
                hired TIMESTAMP,
                salary FLOAT,
                FOREIGN KEY (state) REFERENCES technician_state (id),
                FOREIGN KEY (role) REFERENCES role (id)
            );

            -- Create maintanance_job_state table
            CREATE TABLE IF NOT EXISTS maintanance_job_state (
                id INT PRIMARY KEY,
                repr VARCHAR
            );

            -- Create maintenance_job table
            CREATE TABLE IF NOT EXISTS maintenance_job (
                client_id INT,
                technician_id INT,
                state INT,
                requested TIMESTAMP,
                started TIMESTAMP,
                done TIMESTAMP,
                PRIMARY KEY (client_id, technician_id),
                FOREIGN KEY (client_id) REFERENCES client (id),
                FOREIGN KEY (technician_id) REFERENCES technician (id),
                FOREIGN KEY (state) REFERENCES maintanance_job_state (id)
            );
        ";
    }
}
