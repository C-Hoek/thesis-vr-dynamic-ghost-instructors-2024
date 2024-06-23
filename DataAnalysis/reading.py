import csv
from Vector3 import Vector3


def read_file(file_name):
    """
    This method reads a single trial file. It processes the errors, transparency values,
        ghost and student pointer positions, and the statistics.
    :param file_name: The name of the file that should be read.
    :return: A (error, transparency, ghost_pointer_pos, student_pointer_pos, statistics) tuple.
    """
    num = 0
    transparency = []
    error = []
    ghost_pointer_pos = []
    student_pointer_pos = []
    statistics = []

    with open(file_name, mode='r') as file:
        csvFile = csv.reader(file)

        for lines in csvFile:
            # Skip the trial information
            if num < 13:
                num = num + 1
                continue

            num = num + 1

            if lines[1] == "Error":
                error.append(read_error(lines))
            if lines[1] == "Ghost Transparency":
                transparency.append(read_transparency_line(lines))
            if lines[1] == "Ghost Hand Position":
                ghost_pointer_pos.append(read_ghost_pos(lines))
            if lines[1] == "Student Hand Position":
                student_pointer_pos.append(read_student_pos(lines))
            if lines[1] == "Statistics":
                statistics = read_statistics(lines)

    return error, transparency, ghost_pointer_pos, student_pointer_pos, statistics


def read_transparency_line(line):
    """
    This method processes the ghost transparency lines.
    :param line: The array that contains the line information.
    :return: It returns a (time, transparency) tuple.
    """
    return float(line[0]), float(line[2])


def read_error(line):
    """
    This method processes the error line.
    :param line: The array that contains the line information.
    :return: It returns a (time, error) tuple.
    """
    return float(line[0]), float(line[2])


def read_ghost_pos(line):
    """
    This method processes the ghost position line.
    :param line: The array that contains the line information.
    :return: It returns a (time, position) tuple.
    """
    x, y, z = float(line[6][1:]), float(line[7]), float(line[8][:len(line[8]) - 1])
    return float(line[0]), Vector3(x, y, z)


def read_student_pos(line):
    """
    This method processes the student position line.
    :param line: The array that contains the line information.
    :return: It returns a (time, position) tuple.
    """
    x, y, z = float(line[10][1:]), float(line[11]), float(line[12][:len(line[12]) - 1])
    return float(line[0]), Vector3(x, y, z)


def read_statistics(line):
    """
    This method processes the statistics line.
    :param line: The array that contains the line information.
    :return: It returns a (time, average_error, max_error, min_error, time_to_completion, completed,
        task_performance, learning_effect) tuple.
    """
    average_error = float(line[2][28:])
    max_error = float(line[3][12:])
    min_error = float(line[4][12:])
    time_to_completion = float(line[5][20:])
    completed = True if line[6][13:] == "True" else False
    task_performance = float(line[7][19:])
    learning_effect = float(line[8][18:len(line[8]) - 2])

    return (float(line[0]), average_error, max_error, min_error, time_to_completion,
            completed, task_performance, learning_effect)
