FROM postgres:12-alpine AS build-env
WORKDIR /app

ENV POSTGRES_USER postgres
ENV POSTGRES_PASSWORD patate123
ENV POSTGRES_DB BoulderPOS

# Expose the PostgreSQL port
EXPOSE 5432

# Add VOLUMEs to allow backup of config, logs and databases
VOLUME  ["/etc/postgresql", "/var/log/postgresql", "/var/lib/postgresql/data"]

# Set the default command to run when starting the container
CMD ["postgres"]