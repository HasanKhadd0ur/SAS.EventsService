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
                // Adjust the path to your unit tests folder
                bat 'dotnet test D:\\SAS\\SAS.EventsService\\tests\\SAS.EventsService.Tests.UnitTests --no-build --verbosity normal'
            }
        }

        stage('Integration Tests') {
            steps {
                // Adjust the path to your integration tests folder
                bat 'dotnet test D:\\SAS\\SAS.EventsService\\tests\\SAS.EventsService.Tests.IntegrationTests --no-build --verbosity normal'
            }
        }

        stage('Architecture Validation') {
            steps {
                echo 'Running architecture validation tests...'
                bat 'dotnet test D:\\SAS\\SAS.EventsService\\tests\\SAS.EventsService.Tests.ArchitectureTests --no-build --verbosity normal'
            }
        }

        stage('Docker Build') {
            steps {
                bat 'docker build -t sas-eventsservice:latest -f src\\SAS.EventsService.API\\Dockerfile src\\SAS.EventsService.API'
                bat 'docker build -t sas-identityservice:latest -f src\\SAS.IdentityService.API\\Dockerfile src\\SAS.IdentityService.API'
            }
        }

        stage('Docker Compose Up') {
            steps {
                bat 'docker-compose up -d --build'
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
