pipeline {
    agent any

    environment {
        DOCKER_REGISTRY = 'your-docker-registry'
        EVENTS_IMAGE = "${DOCKER_REGISTRY}/sas-eventsservice:latest"
        IDENTITY_IMAGE = "${DOCKER_REGISTRY}/sas-identityservice:latest"
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore & Build') {
            steps {
                bat 'dotnet restore'
                bat 'dotnet build --configuration Release'
            }
        }

        stage('Unit Tests') {
            steps {
                bat 'dotnet test D:\\SAS\\SAS.EventsService\\tests\\SAS.EventsService.Tests.UnitTests --no-build --no-restore --verbosity normal'
            }
        }

        stage('Integration Tests') {
            steps {
                bat 'dotnet test D:\\SAS\\SAS.EventsService\\tests\\SAS.EventsService.Tests.IntegrationTests --no-build --no-restore --verbosity normal'
            }
        }

        stage('Architecture Validation') {
            steps {
                echo 'Running architecture validation tests...'
                bat 'dotnet test D:\\SAS\\SAS.EventsService\\tests\\SAS.EventsService.Tests.ArchitectureTests --no-build --no-restore --verbosity normal'
            }
        }

        stage('Docker Build') {
            steps {
                // bat 'timeout /t 120 /nobreak'
                  bat 'ping 127.0.0.1 -n 121 > nul'
                // bat 'docker build -t sas-eventsservice:latest -f src\\SAS.EventsService.API\\Dockerfile .'
                 bat 'echon "docker build -t sas-eventsservice:latest -f src\\SAS.EventsService.API\\Dockerfile ."'
            
            }
        }

        stage('Docker Compose Up') {
            steps {
                // bat 'timeout /t 120 /nobreak'
                  bat 'ping 127.0.0.1 -n 121 > nul'
                bat 'echo "docker-compose up -d --build"'
            }
        }
    }

    post {
        always {
            echo 'Cleaning up...'
            bat 'docker-compose down'
            bat 'docker system prune -f'
        }
    }
}
