#!/bin/bash

# Script to regenerate IHaloInfiniteClient interface from HaloInfiniteClient class
# This script should be run whenever public methods are added or modified in HaloInfiniteClient

echo "Regenerating IHaloInfiniteClient interface..."

# Check if we're in the right directory
if [ ! -f "Grunt/Grunt/Core/HaloInfiniteClient.cs" ]; then
    echo "Error: Please run this script from the repository root directory"
    exit 1
fi

# Save the current directory
REPO_ROOT=$(pwd)

# Create a temporary directory for the generator
TEMP_DIR=$(mktemp -d)
cd "$TEMP_DIR"

# Create a simple console app and copy our generator
dotnet new console --force > /dev/null 2>&1
cp "$REPO_ROOT/Tools/InterfaceGenerator.cs" Program.cs

# Run the generator
echo "Running interface generator..."
dotnet run > /dev/null 2>&1

if [ $? -eq 0 ]; then
    echo "Interface regenerated successfully!"
    echo "The IHaloInfiniteClient interface has been updated to match all public methods in HaloInfiniteClient"
else
    echo "Error: Failed to regenerate interface"
    exit 1
fi

# Clean up
rm -rf "$TEMP_DIR"

echo "Done!"